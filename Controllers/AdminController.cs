using DreamDay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            var userList = new List<(ApplicationUser user, string role)>();

            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                userList.Add((u, roles.FirstOrDefault() ?? "N/A"));
            }

            return View(userList); ;
        }
    }
}
