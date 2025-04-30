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
using Microsoft.AspNetCore.Identity;

namespace DreamDay.Controllers
{
    public class VendorReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVendorReviewService _vendorReviewService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public VendorReviewsController(
            ApplicationDbContext context, 
            IVendorReviewService vendorReviewService,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _vendorReviewService = vendorReviewService;
            _signInManager = signInManager;
        }

        // GET: VendorReviews
        public async Task<IActionResult> Index()
        {
            var vendorReview = _vendorReviewService.GeAlltVendorReviews();
            return View(vendorReview);
        }

        // GET: VendorReviews/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

           var vendorReview = _vendorReviewService.GetVendorReviewById(id);

            if (vendorReview == null)
            {
                return NotFound();
            }

            return View(vendorReview);
        }

        // GET: VendorReviews/Create
        public IActionResult Create(int vendorId)
        {
            ViewData["VendorID"] = vendorId;
            return View();
        }

        // POST: VendorReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserID,VendorID,Message,Rating,date")] VendorReviews vendorReviews)
        {
            ModelState.Remove("date");
            ModelState.Remove("UserID");
            ModelState.Remove("User");
            ModelState.Remove("Vendor");
            if (ModelState.IsValid)
            {
                var SignedInUser = _signInManager.UserManager.GetUserAsync(User).Result;
                vendorReviews.UserID = SignedInUser?.Id;
                _vendorReviewService.AddVendorReview(vendorReviews);
                return RedirectToAction("Index", "VendorPackageBookings");
            }
            return View(vendorReviews);
        }

        // GET: VendorReviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

             var vendorReview = _vendorReviewService.GetVendorReviewById(id.Value);
            if (vendorReview == null)
            {
                return NotFound();
            }

            return View(vendorReview);
        }

        // POST: VendorReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _vendorReviewService.DeleteVendorReview(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
