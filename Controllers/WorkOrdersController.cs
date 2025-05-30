using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProductionManagementSystem.Data;
using ProductionManagementSystem.Models;

namespace ProductionManagementSystem.Controllers
{
    public class WorkOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkOrders
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkOrders.ToListAsync());
        }

        // GET: WorkOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // GET: WorkOrders/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _context.Products.ToListAsync();
            ViewBag.ProductionLines = await _context.ProductionLines.ToListAsync() ?? new List<ProductionLine>();
            var workOrder = new WorkOrder();
            return View(workOrder);
        }

        // POST: WorkOrders/Create
        // POST: WorkOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkOrder workOrder)
        {
            ViewBag.Products = await _context.Products.ToListAsync(); // Добавляем эту строку
            ViewBag.ProductionLines = await _context.ProductionLines.ToListAsync() ?? new List<ProductionLine>();
            if (ModelState.IsValid)
            {
                // Получите продукт из базы данных
                var product = await _context.Products.FindAsync(workOrder.ProductId);
                if (product != null)
                {
                    // Установите время производства
                    workOrder.ProductionTimePerUnit = product.ProductionTimePerUnit;

                    // Автоматический расчет EstimatedEndDate
                    double efficiencyFactor = 1.0; // Default efficiency factor
                    if (workOrder.ProductionLineId.HasValue)
                    {
                        var productionLine = await _context.ProductionLines.FindAsync(workOrder.ProductionLineId);
                        if (productionLine != null)
                        {
                            efficiencyFactor = productionLine.EfficiencyFactor;
                        }
                        else
                        {
                            // Обработка случая, когда производственная линия не найдена
                            ModelState.AddModelError("ProductionLineId", "Selected production line not found.");
                            return View(workOrder); // Верните представление с сообщением об ошибке
                        }
                    }

                    double productionTimeInMinutes = (workOrder.Quantity * workOrder.ProductionTimePerUnit) / efficiencyFactor;
                    workOrder.EstimatedEndDate = workOrder.StartDate.AddMinutes(productionTimeInMinutes);
                }

                _context.WorkOrders.Add(workOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder == null)
            {
                return NotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,Quantity,StartDate,EstimatedEndDate,Status")] WorkOrder workOrder)
        {
            if (id != workOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderExists(workOrder.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _context.WorkOrders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workOrder = await _context.WorkOrders.FindAsync(id);
            if (workOrder != null)
            {
                _context.WorkOrders.Remove(workOrder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderExists(int id)
        {
            return _context.WorkOrders.Any(e => e.Id == id);
        }
    }
}