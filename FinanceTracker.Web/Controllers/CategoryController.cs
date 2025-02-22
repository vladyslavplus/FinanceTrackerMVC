using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;

namespace FinanceTracker.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Category
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Category/AddOrEdit
        [Authorize]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if(id == 0) 
                return View(new Category());
            else
                return View(await _context.Categories.FindAsync(id));
        }

        // POST: Category/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddOrEdit([Bind("Id,Title,Icon,Type")] Category category)
        {
            if (ModelState.IsValid)
            {
                if(category.Id == 0)
                {
                    _context.Add(category);
                    TempData["SuccessMessage"] = "Category added successfully!";
                }
                else
                {
                    _context.Update(category);
                    TempData["SuccessMessage"] = "Category updated successfully!";
                }
                   

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                TempData["SuccessMessage"] = "Category deleted successfully!";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
