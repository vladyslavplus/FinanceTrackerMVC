using System.Diagnostics;
using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Data;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(new Messages());
        }
        [HttpPost]
        public async Task<IActionResult> Index(Messages message)
        {
            if (ModelState.IsValid)
            {
                await _context.Messages.AddAsync(message);
                TempData["SuccessMessage"] = "Message sent successfully!";
                await _context.SaveChangesAsync();
                ModelState.Clear();
                return View(new Messages());
            }
            return View(message);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
