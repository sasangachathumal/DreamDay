using DreamDay.Business.Interface;
using DreamDay.Data;
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
        private readonly IVendorService _vendorService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AdminController(IVendorService vendorService, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _vendorService = vendorService;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Dashboard()
        {
            var allUsers = await _userManager.Users.ToListAsync();

            var totalVendors = _vendorService.GetAllVendors().Count;
            var totalUsers = allUsers.Count(u => !_userManager.IsInRoleAsync(u, "Admin").Result);
            var totalPlanners = allUsers.Count(u => _userManager.IsInRoleAsync(u, "Planner").Result);

            var totalWeddings = await _context.Weddings.CountAsync(); // ← updated line

            var topVendors = _vendorService.GetAllVendors()
                                .OrderByDescending(v => v.VendorPackages?.Count)
                                .Take(3)
                                .ToList();

            ViewBag.TotalVendors = totalVendors;
            ViewBag.TotalUsers = totalUsers;
            ViewBag.TotalPlanners = totalPlanners;
            ViewBag.TotalWeddings = totalWeddings; // ← updated ViewBag
            ViewBag.TopVendors = topVendors;

            return View();
        }

    }
}
