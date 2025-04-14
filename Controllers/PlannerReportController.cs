using DreamDay.Business.Interface;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DreamDay.Controllers
{
    public class PlannerReportController : Controller
    {
        private readonly IVendorService _vendorService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IVendorPackageBookingService _vendorPackageBookingService;
        private readonly IBudgetService _budgetService;

        public PlannerReportController(
            IVendorService vendorService, 
            UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context,
            IVendorPackageBookingService vendorPackageBookingService,
            IBudgetService budgetService)
        {
            _vendorService = vendorService;
            _userManager = userManager;
            _context = context;
            _vendorPackageBookingService = vendorPackageBookingService;
            _budgetService = budgetService;
        }
        public IActionResult Index()
        {
            var topBookingPackages = _vendorPackageBookingService.GetAllVendor()
                                    .GroupBy(v => v.VendorPackageID)
                                    .OrderByDescending(g => g.Count())
                                    .SelectMany(g => g)
                                    .ToList();
            var averageBudgets = _budgetService.GetAllBudgets()
                                    .GroupBy(v => v.Category)
                                    .Select(g => new {
                                        Category = g.Key,
                                        AverageAmount = g.Average(b => b.AllocatedAmount)
                                    })
                                    .ToList();
            ViewBag.topBookingPackages = topBookingPackages;
            ViewBag.averageBudgets = averageBudgets;

            return View();
        }
    }
}
