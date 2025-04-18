using DreamDay.Business.Interface;
using DreamDay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Planner")]
    public class PlannerController : Controller
    {
        private readonly IWeddingService _weddingService;
        private readonly IChecklistItemService _checklistItemService;
        private readonly IGuestService _guestService;
        private readonly ITimelineService _timelineService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IVendorService _vendorService;
        private readonly IVendorPackageService _vendorPackageService;
        private readonly IVendorPackageBookingService _vendorPackageBookingService;
        private readonly IVendorCategoryService _vendorCategoryService;

        public PlannerController(
            IWeddingService weddingService,
            SignInManager<ApplicationUser> signInManager,
            IChecklistItemService checklistItemService,
            IGuestService guestService,
            ITimelineService timelineService,
            IVendorService vendorService,
            IVendorPackageService vendorPackageService,
            IVendorPackageBookingService vendorPackageBookingService,
            IVendorCategoryService vendorCategoryService)
        {
            _signInManager = signInManager;
            _weddingService = weddingService;
            _checklistItemService = checklistItemService;
            _guestService = guestService;
            _timelineService = timelineService;
            _vendorService = vendorService;
            _vendorPackageService = vendorPackageService;
            _vendorPackageBookingService = vendorPackageBookingService;
            _vendorCategoryService = vendorCategoryService;
        }
        public IActionResult Dashboard()
        {
            var SignedInUser = _signInManager.UserManager.GetUserAsync(User).Result;
            var weddings = _weddingService.GetWeddingByPlannerId(SignedInUser?.Id);

            if (weddings == null)
            {
                return NotFound();
            }

            if (weddings.Count > 0)
            {
                foreach (var wedding in weddings)
                {
                    wedding.ChecklistItems = _checklistItemService.GetChecklistItemsByWeddingId(wedding.Id);
                    wedding.Guests = _guestService.GetGuestsByWeddingId(wedding.Id);
                    wedding.Timelines = _timelineService.GetTimelinesByWeddingId(wedding.Id);
                }
            }

            return View(weddings);
        }

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var wedding = _weddingService.GetWeddingById(id);
            if (wedding == null)
            {
                return NotFound();
            }
            foreach (var booking in wedding?.VendorPackageBookings)
            {
                booking.VendorPackage = _vendorPackageService.GetVendorPackageById(booking.VendorPackageID);
                booking.VendorPackage.Vendor.VendorCategory = _vendorCategoryService.GetVendorCategoryById(booking.VendorPackage.Vendor.VendorCategoryId);
            }

            return View(wedding);
        }

        public IActionResult MarkAsDone(int itemId)
        {
            if (_checklistItemService.MarkAsDone(itemId))
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public IActionResult MarkRimelineAsDone(int itemId)
        {
            if (_timelineService.MarkAsDone(itemId))
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }
    }
}
