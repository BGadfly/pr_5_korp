using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionManagementSystem.Data;
using ProductionManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductionLinesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductionLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/lines?available=true
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductionLine>>> GetProductionLines(bool available = false)
        {
            IQueryable<ProductionLine> query = _context.ProductionLines;

            if (available)
            {
                query = query.Where(l => l.CurrentWorkOrderId == null);
            }

            return await query.ToListAsync();
        }

        // PUT: api/lines/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> PutLineStatus(int id, string status)
        {
            var line = await _context.ProductionLines.FindAsync(id);

            if (line == null)
            {
                return NotFound();
            }

            line.Status = status;

            _context.Entry(line).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductionLineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ProductionLineExists(int id)
        {
            return _context.ProductionLines.Any(e => e.Id == id);
        }
    }
}