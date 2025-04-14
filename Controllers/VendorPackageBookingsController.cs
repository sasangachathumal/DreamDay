using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamDay.Data;
using DreamDay.Models;
using DreamDay.Business.Interface;

namespace DreamDay.Controllers
{
    public class VendorPackageBookingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVendorPackageBookingService _vendorPackageBookingService;
        private readonly IWeddingService _weddingService;
        private readonly IVendorPackageService _vendorPackageService;
        private readonly IVendorService _vendorService;
        private readonly IVendorImageService _vendorImageService;
        private readonly IVendorReviewService _vendorReviewService;
        private readonly IBudgetService _budgetService;
        private readonly IVendorCategoryService _vendorCategoryService;

        public VendorPackageBookingsController(
            ApplicationDbContext context, 
            IVendorPackageBookingService vendorPackageBookingService,
            IWeddingService weddingService,
            IVendorPackageService vendorPackageService,
            IVendorService vendorService,
            IVendorImageService vendorImageService,
            IVendorReviewService vendorReviewService,
            IBudgetService budgetService,
            IVendorCategoryService vendorCategoryService)
        {
            _context = context;
            _vendorPackageBookingService = vendorPackageBookingService;
            _weddingService = weddingService;
            _vendorPackageService = vendorPackageService;
            _vendorService = vendorService;
            _vendorImageService = vendorImageService;
            _vendorReviewService = vendorReviewService;
            _budgetService = budgetService;
            _vendorCategoryService = vendorCategoryService;
        }

        // GET: VendorPackageBookings
        public async Task<IActionResult> Index()
        {
            int _WeddingId = 0;
            var sessionWeddingId = HttpContext.Session.GetInt32("WeddingId");
            if (sessionWeddingId.HasValue)
            {
                _WeddingId = sessionWeddingId.Value;
            }
            else
            {
                _WeddingId = 0;
            }
            var packageBookings = _vendorPackageBookingService.GetVendorPackageBookingsByWeddingId(_WeddingId);
            if (packageBookings != null) 
            {
                foreach (var packageBooking in packageBookings)
                {
                    packageBooking.Wedding = _weddingService.GetWeddingById(packageBooking.WeddingID);
                    packageBooking.VendorPackage = _vendorPackageService.GetVendorPackageById(packageBooking.VendorPackageID);
                    packageBooking.VendorPackage.Vendor = _vendorService.GetVendorById(packageBooking.VendorPackage.VendorId);
                    packageBooking.VendorPackage.Vendor.VendorImages = _vendorImageService.GetAppImagesByVendorID(packageBooking.VendorPackage.VendorId);
                }
            }
            return View(packageBookings);
        }

        // GET: VendorPackageBookings/Create
        public async Task<IActionResult> Create()
        {
            var vendors = _vendorService.GetAllVendors();
            if (vendors != null)
            {
                foreach (var vendor in vendors)
                {
                    vendor.VendorPackages = _vendorPackageService.GetVendorPackagesByVendorId(vendor.Id);
                    vendor.VendorReviews = _vendorReviewService.GetVendorReviewsByVendorId(vendor.Id);
                    vendor.VendorImages = _vendorImageService.GetAppImagesByVendorID(vendor.Id);
                }
            }
            return View(vendors);
        }

        public async Task<IActionResult> ReviewView(Vendor vendor)
        {
            if (vendor != null)
            {
                vendor.VendorReviews = _vendorReviewService.GetVendorReviewsByVendorId(vendor.Id);
            }

            return View(vendor);
        }

        public async Task<IActionResult> BookedVendorReview(Vendor vendor)
        {
            if (vendor != null)
            {
                vendor.VendorReviews = _vendorReviewService.GetVendorReviewsByVendorId(vendor.Id);
            }

            return View(vendor);
        }

        public async Task<IActionResult> PackageView(Vendor vendor)
        {
            if (vendor != null)
            {
                vendor.VendorPackages = _vendorPackageService.GetVendorPackagesByVendorId(vendor.Id);
            }
            return View(vendor);
        }

        // POST: VendorPackageBookings/Book
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(int vendorPackageId, int vendorId)
        {
            if (vendorPackageId != 0 && vendorId != 0)
            {
                int _WeddingId = 0;
                var sessionWeddingId = HttpContext.Session.GetInt32("WeddingId");
                if (sessionWeddingId.HasValue)
                {
                    _WeddingId = sessionWeddingId.Value;
                }
                else
                {
                    _WeddingId = 0;
                }

                var wedding = _weddingService.GetWeddingById(_WeddingId);
                var vendor = _vendorService.GetVendorById(vendorId);
                var weddingBudget = _budgetService.GetBudgetsByWeddingId(wedding.Id);
                var vendorPackage = _vendorPackageService.GetVendorPackageById(vendorPackageId);

                if (vendor != null)
                {
                    vendor.VendorCategory = _vendorCategoryService.GetVendorCategoryById(vendor.VendorCategoryId);
                    var packageBooking = new VendorPackageBooking
                    {
                        VendorPackageID = vendorPackageId,
                        WeddingID = wedding.Id,
                        BookDate = wedding.WeddingDate
                    };

                    if (weddingBudget != null)
                    {
                        foreach (var item in weddingBudget)
                        {
                            if (item.Category.ToString() == vendor.VendorCategory.Name)
                            {
                                item.SpendAmount += vendorPackage.Price;
                                _budgetService.UpdateBudget(item);
                            }
                        }
                    }

                    _vendorPackageBookingService.AddVendorPackageBooking(packageBooking);
                }

                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: VendorPackageBookings/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            _vendorPackageBookingService.DeleteVendorPackageBooking(id);

            return RedirectToAction(nameof(Index));
        }

        // POST: VendorPackageBookings/Confirm/5
        public async Task<IActionResult> Confirm(int id)
        {
            _vendorPackageBookingService.ConfirmVendorPackageBooking(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
