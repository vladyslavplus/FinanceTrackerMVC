using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;

        public CategoryController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Category
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return View(categories);
        }

        // GET: Category/AddOrEdit
        [Authorize]
        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if(id == 0) 
                return View(new Category());
            else
            {
                var userId = _userManager.GetUserId(User);
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

                if (category == null)
                    return NotFound();

                return View(category);
            }
        }

        // POST: Category/AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddOrEdit([Bind("Id,Title,Icon,Type")] Category category)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (category.Id == 0)
                {
                    category.UserId = userId; 
                    _context.Add(category);
                    TempData["SuccessMessage"] = "Category added successfully!";
                }
                else
                {
                    var existingCategory = await _context.Categories
                        .FirstOrDefaultAsync(c => c.Id == category.Id && c.UserId == userId);

                    if (existingCategory == null)
                        return NotFound(); 

                    existingCategory.Title = category.Title;
                    existingCategory.Icon = category.Icon;
                    existingCategory.Type = category.Type;

                    _context.Update(existingCategory);
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
            var userId = _userManager.GetUserId(User);
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (category == null)
                return NotFound(); 

            _context.Categories.Remove(category);
            TempData["SuccessMessage"] = "Category deleted successfully!";
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
