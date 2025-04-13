using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamDay.Data;
using DreamDay.Models;

namespace DreamDay.Controllers
{
    public class VendorReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VendorReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VendorReviews
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VendorReviews.Include(v => v.User).Include(v => v.Vendor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: VendorReviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorReviews = await _context.VendorReviews
                .Include(v => v.User)
                .Include(v => v.Vendor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorReviews == null)
            {
                return NotFound();
            }

            return View(vendorReviews);
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
            ModelState.Remove("VendorID");
            if (ModelState.IsValid)
            {
                _context.Add(vendorReviews);
                await _context.SaveChangesAsync();
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

            var vendorReviews = await _context.VendorReviews
                .Include(v => v.User)
                .Include(v => v.Vendor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorReviews == null)
            {
                return NotFound();
            }

            return View(vendorReviews);
        }

        // POST: VendorReviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorReviews = await _context.VendorReviews.FindAsync(id);
            if (vendorReviews != null)
            {
                _context.VendorReviews.Remove(vendorReviews);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
