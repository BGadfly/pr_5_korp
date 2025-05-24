using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionManagementSystem.Data;
using ProductionManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductionManagementSystem.Services;

namespace ProductionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductionCalculationService _productionCalculationService;

        public OrdersController(ApplicationDbContext context, IProductionCalculationService productionCalculationService)
        {
            _context = context;
            _productionCalculationService = productionCalculationService;
        }

        // GET: api/orders?status=active&date=today
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkOrder>>> GetOrders(string status, string date)
        {
            IQueryable<WorkOrder> query = _context.WorkOrders;

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }

            if (!string.IsNullOrEmpty(date) && date == "today")
            {
                query = query.Where(o => o.StartDate.Date == DateTime.Today);
            }

            return await query.ToListAsync();
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<WorkOrder>> PostWorkOrder(WorkOrder workOrder)
        {
            //Проверяем материалы

            // Получаем производственную линию (если указана)
            ProductionLine? line = null;
            if (workOrder.ProductionLineId.HasValue)
            {
                line = await _context.ProductionLines.FindAsync(workOrder.ProductionLineId.Value);
                if (line == null)
                {
                    return BadRequest("Invalid production line ID.");
                }
            }

            // Рассчитываем EstimatedEndDate с использованием сервиса
            float efficiencyFactor = line?.EfficiencyFactor ?? 1.0f;
            double productionTime = _productionCalculationService.CalculateProductionTime(workOrder.ProductId, workOrder.Quantity, (int?)workOrder.ProductionLineId);
            workOrder.EstimatedEndDate = workOrder.StartDate.AddMinutes(productionTime);

            // Сохраняем заказ в базе данных
            _context.WorkOrders.Add(workOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWorkOrder), new { id = workOrder.Id }, workOrder);
        }

        // GET: api/orders/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkOrder>> GetWorkOrder(int id)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);

            if (workOrder == null)
            {
                return NotFound();
            }

            return workOrder;
        }
    }
}