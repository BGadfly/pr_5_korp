using Microsoft.AspNetCore.Mvc;
using ProductionManagementSystem.Data;
using ProductionManagementSystem.Models;
using System.Threading.Tasks;

namespace ProductionManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CalculateController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/calculate/production
        [HttpPost("production")]
        public async Task<ActionResult<double>> CalculateProductionTime(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return BadRequest("Invalid product ID.");
            }

            double productionTime = (quantity * product.ProductionTimePerUnit); // Basic calculation.

            return productionTime;
        }
    }
}
