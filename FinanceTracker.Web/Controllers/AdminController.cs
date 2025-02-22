using FinanceTracker.Domain.Entities;
using FinanceTracker.Infrastructure.Data;
using FinanceTracker.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Users> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ApplicationDbContext context, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersWithRoles = new List<UserWithRoles>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add(new UserWithRoles
                {
                    User = user,
                    Roles = roles
                });
            }

            return View(usersWithRoles);
        }
        [HttpGet]
        public async Task<IActionResult> AddOrEdit(string? id)
        {
            var roles = await _roleManager.Roles
                .Select(role => new SelectListItem { Value = role.Name, Text = role.Name })
                .ToListAsync();

            if(id == null)
                return View(new AddOrEditUserViewModel { Roles = roles });

            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            return View(new AddOrEditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                SelectedRole = userRoles.FirstOrDefault(),
                Roles = roles
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(AddOrEditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(model.Id))
                {
                    var user = new Users
                    {
                        UserName = model.UserName,
                        Email = model.Email
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, model.SelectedRole);
                        TempData["SuccessMessage"] = "User created successfully!";
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    var user = await _userManager.FindByIdAsync(model.Id);
                    if (user == null) return NotFound();

                    user.UserName = model.UserName;
                    user.Email = model.Email;

                    var existingRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, existingRoles);
                    await _userManager.AddToRoleAsync(user, model.SelectedRole);

                    var updateResult = await _userManager.UpdateAsync(user);
                    if (updateResult.Succeeded)
                    {
                        TempData["SuccessMessage"] = "User updated successfully!";
                        return RedirectToAction("Index");
                    }
                }
            }
         
            model.Roles = await _roleManager.Roles
                .Select(role => new SelectListItem { Value = role.Name, Text = role.Name })
                .ToListAsync();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "User deleted successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Messages()
        {
            return View(await _context.Messages.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> MessageDetails(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return RedirectToAction("Messages");
            }
            return View(message);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message != null)
            {
                _context.Messages.Remove(message);
                TempData["SuccessMessage"] = "Message deleted successfully!";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Messages");
        }

    }
}
