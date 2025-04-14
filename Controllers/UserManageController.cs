using DreamDay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

    namespace DreamDay.Controllers
    {
        [Authorize(Roles = "Admin")]
        public class UserManageController : Controller
        {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;

            public UserManageController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<IActionResult> ManageUsers()
            {
                var users = await _userManager.Users.ToListAsync();
                var userList = new List<(ApplicationUser user, string role)>();

                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    userList.Add((user, roles.FirstOrDefault() ?? "N/A"));
                }

                return View(userList);
            }

        // GET: Edit User
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "N/A";

            ViewBag.Roles = new SelectList(_roleManager.Roles.Select(r => r.Name));
            return View("UserManagementEdit", (user, role));
        }

        // POST: Save Edited User
        [HttpPost]
        public async Task<IActionResult> EditUser(ApplicationUser model, string role)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user == null) return NotFound();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles.ToArray());
                await _userManager.AddToRoleAsync(user, role);

                return RedirectToAction("ManageUsers");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ViewBag.Roles = new SelectList(_roleManager.Roles.Select(r => r.Name));
            return View("UserManagementEdit", (user, role));
        }

        // DELETE
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction("ManageUsers");
        }



    }
}
