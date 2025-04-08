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
    public class VendorsCatalogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVendorService _vendorService;
        private readonly IVendorPackageService _vendorPackageService;
        private readonly IVendorReviewService _vendorReviewService;

        public VendorsCatalogController(
            ApplicationDbContext context,
            IVendorService vendorService,
            IVendorPackageService vendorPackageService,
            IVendorReviewService vendorReviewService)
        {
            _context = context;
            _vendorService = vendorService;
            _vendorPackageService = vendorPackageService;
            _vendorReviewService = vendorReviewService;
        }

        // GET: VendorsCatalog
        public async Task<IActionResult> Index()
        {
            // Get all vendors with their categories
            var vendors = _vendorService.GetAllVendors();

            if (vendors != null && vendors.Count >0)
            {
                foreach (var vendor in vendors)
                {
                    // Get the vendor's packages
                    var packages = _vendorPackageService.GetVendorPackageById(vendor.Id);
                    // Check if packages are not null
                    if (packages != null)
                    {
                        // Assign the packages to the vendor
                        vendor.VendorPackages = new List<VendorPackage> { packages };
                    }
                    // Get the vendor's reviews
                    var reviews = _vendorReviewService.GetVendorReviewById(vendor.Id);
                    // Check if reviews are not null
                    if (reviews != null)
                    {
                        // Assign the reviews to the vendor
                        vendor.VendorReviews = new List<VendorReviews> { reviews };
                    }
                }
            }

            return View(vendors);
        }

        // GET: VendorsCatalog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .Include(v => v.VendorCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // GET: VendorsCatalog/Create
        public IActionResult Create()
        {
            ViewData["VendorCategoryId"] = new SelectList(_context.VendorCategories, "Id", "Name");
            return View();
        }

        // POST: VendorsCatalog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VendorCategoryId,Name,Description,Phone,Email,Address")] Vendor vendor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorCategoryId"] = new SelectList(_context.VendorCategories, "Id", "Name", vendor.VendorCategoryId);
            return View(vendor);
        }

        // GET: VendorsCatalog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            ViewData["VendorCategoryId"] = new SelectList(_context.VendorCategories, "Id", "Name", vendor.VendorCategoryId);
            return View(vendor);
        }

        // POST: VendorsCatalog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VendorCategoryId,Name,Description,Phone,Email,Address")] Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorCategoryId"] = new SelectList(_context.VendorCategories, "Id", "Name", vendor.VendorCategoryId);
            return View(vendor);
        }

        // GET: VendorsCatalog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .Include(v => v.VendorCategory)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: VendorsCatalog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor != null)
            {
                _context.Vendors.Remove(vendor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}
