using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductionManagementSystem.Data;
using ProductionManagementSystem.Models;
using ProductionManagementSystem.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace ProductionManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? category, string? searchTerm)
        {
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.Name.Contains(searchTerm));
            }

            var viewModel = new ProductListViewModel
            {
                Products = await products.ToListAsync(),
                Categories = await _context.Products.Select(p => p.Category).Distinct().ToListAsync(),
                SelectedCategory = category, // Pass the selected category to the view
                SearchTerm = searchTerm   // Pass the search term to the view
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || id <= 0)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}