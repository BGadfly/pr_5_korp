using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionManagementSystem.Data;
using ProductionManagementSystem.Models;

namespace ProductionManagementSystem.Controllers
{
    public class ProductionLinesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductionLinesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductionLines
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductionLines.ToListAsync());
        }

        // GET: ProductionLines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionLine = await _context.ProductionLines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productionLine == null)
            {
                return NotFound();
            }

            return View(productionLine);
        }

        // GET: ProductionLines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductionLines/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,EfficiencyFactor")] ProductionLine productionLine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productionLine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productionLine);
        }

        // GET: ProductionLines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionLine = await _context.ProductionLines.FindAsync(id);
            if (productionLine == null)
            {
                return NotFound();
            }
            return View(productionLine);
        }

        // POST: ProductionLines/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Status,EfficiencyFactor")] ProductionLine productionLine)
        {
            if (id != productionLine.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productionLine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductionLineExists(productionLine.Id))
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
            return View(productionLine);
        }

        // GET: ProductionLines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productionLine = await _context.ProductionLines
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productionLine == null)
            {
                return NotFound();
            }

            return View(productionLine);
        }

        // POST: ProductionLines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productionLine = await _context.ProductionLines.FindAsync(id);
            if (productionLine != null)
            {
                _context.ProductionLines.Remove(productionLine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductionLineExists(int id)
        {
            return _context.ProductionLines.Any(e => e.Id == id);
        }
    }
}