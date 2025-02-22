using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.Web.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;

        public TransactionController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Transaction
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Transactions.Include(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }


        // GET: Transaction/Create
        [Authorize]
        public IActionResult AddOrEdit(int id = 0)
        {
            PopulateCategories();
            if(id == 0)
                return View(new Transaction());
            else
                return View(_context.Transactions.Find(id));
        }

        // POST: Transaction/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddOrEdit([Bind("Id,CategoryId,Amount,Note,Date")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                if(transaction.Id == 0)
                {
                    _context.Add(transaction);
                    var user = await _userManager.GetUserAsync(User);
                    user.TransactionsCount++;
                    TempData["SuccessMessage"] = "Transaction added successfully!";
                }
                else
                {
                    _context.Update(transaction);
                    TempData["SuccessMessage"] = "Transaction updated successfully!";
                }    
                    

                await _context.SaveChangesAsync();
                var userToUpdate = await _userManager.GetUserAsync(User);
                _context.Users.Update(userToUpdate);

                return RedirectToAction(nameof(Index));
            }
            PopulateCategories();
            return View(transaction);
        }
        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                var user = await _userManager.GetUserAsync(User);

                user.TransactionsCount--;

                _context.Transactions.Remove(transaction);
                _context.Users.Update(user);
                TempData["SuccessMessage"] = "Transaction deleted successfully!";
                await _context.SaveChangesAsync();           
            }
            
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        public void PopulateCategories()
        {
            var categoryCollection = _context.Categories.ToList();
            Category DefaultCategory = new Category()
            {
                Id = 0,
                Title = "Choose a Category"
            };
            categoryCollection.Insert(0, DefaultCategory);
            ViewBag.Categories = categoryCollection;
        }

    }
}
