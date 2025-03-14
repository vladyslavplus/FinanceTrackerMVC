using System.Globalization;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<Users> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return NotFound();

            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Transaction> transactions = await _context.Transactions
                .AsNoTracking()
                .Include(x => x.Category)
                .Where(y => y.UserId == user.Id && y.Date >= StartDate && y.Date <= EndDate)
                .ToListAsync();

            decimal TotalIncome = transactions
                .Where(i => i.Category.Type == "Income")
                .Sum(j => j.Amount);

            ViewBag.TotalIncome = TotalIncome.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));

            decimal TotalExpense = transactions
                .Where(i => i.Category.Type == "Expense")
                .Sum(j => j.Amount);

            ViewBag.TotalExpense = TotalExpense.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));

            decimal Balance = TotalIncome - TotalExpense;
            ViewBag.Balance = Balance.ToString("C2", CultureInfo.CreateSpecificCulture("en-US"));

            ViewBag.DonutChartDate = transactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Category.Id)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Title, // x-value
                    amount = k.Sum(j => j.Amount),                                                    // y-value
                    formattedAmount = k.Sum(j => j.Amount).ToString("C2", CultureInfo.CreateSpecificCulture("en-US")),
                })
                .OrderByDescending(l => l.amount)
                .ToList();

            List<SplineChartData> incomeSummary = transactions
                .Where(i => i.Category.Type == "Income")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("yyyy-MM-dd"),
                    income = k.Sum(l => l.Amount)
                })
                .ToList();

            List<SplineChartData> expenseSummary = transactions
                .Where(i => i.Category.Type == "Expense")
                .GroupBy(j => j.Date)
                .Select(k => new SplineChartData()
                {
                    day = k.First().Date.ToString("yyyy-MM-dd"),
                    expense = k.Sum(l => l.Amount)
                })
                .ToList();

            var data = incomeSummary.Concat(expenseSummary)
                .GroupBy(i => i.day)
                .Select(j => new SplineChartData()
                {
                    day = j.Key,
                    income = j.Sum(x => x.income),
                    expense = j.Sum(x => x.expense)
                })
                .OrderBy(k => k.day)
                .ToList();


            ViewBag.RecentTransactions = await _context.Transactions
                .Include(i => i.Category)
                .Where(t => t.UserId == user.Id)
                .OrderByDescending(j => j.Date.Date)
                .Take(5)
                .ToListAsync();


            return View(data);
        }
    }

    public class SplineChartData
    {
        public string day { get; set; }
        public decimal income { get; set; } = 0;
        public decimal expense { get; set; } = 0;
    }

}
