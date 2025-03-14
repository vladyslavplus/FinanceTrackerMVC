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
            var userId = _userManager.GetUserId(User);
            var transactions = await _context.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .ToListAsync();

            return View(transactions);
        }


        // GET: Transaction/Create
        [Authorize]
        public IActionResult AddOrEdit(int id = 0)
        {
            PopulateCategories();
            if (id == 0)
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
                var user = await _userManager.GetUserAsync(User);

                if (transaction.Id == 0)
                {
                    transaction.UserId = user.Id;

                    _context.Add(transaction);
                    user.TransactionsCount++; 
                    TempData["SuccessMessage"] = "Transaction added successfully!";
                }
                else
                {
                    var existingTransaction = await _context.Transactions.AsNoTracking()
                        .FirstOrDefaultAsync(t => t.Id == transaction.Id);

                    if (existingTransaction == null || existingTransaction.UserId != user.Id)
                    {
                        return NotFound();
                    }

                    transaction.UserId = user.Id;
                    _context.Update(transaction); 
                    TempData["SuccessMessage"] = "Transaction updated successfully!";
                }

                
                await _context.SaveChangesAsync();
                _context.Users.Update(user);

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
            var userId = _userManager.GetUserId(User);
            var categoryCollection = _context.Categories
                .Where(c => c.UserId == userId)
                .ToList();

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
