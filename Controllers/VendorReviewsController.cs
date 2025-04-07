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
        public IActionResult Create()
        {
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["VendorID"] = new SelectList(_context.Vendors, "Id", "Address");
            return View();
        }

        // POST: VendorReviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserID,VendorID,Message,Rating,date")] VendorReviews vendorReviews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorReviews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", vendorReviews.UserID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "Id", "Address", vendorReviews.VendorID);
            return View(vendorReviews);
        }

        // GET: VendorReviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorReviews = await _context.VendorReviews.FindAsync(id);
            if (vendorReviews == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", vendorReviews.UserID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "Id", "Address", vendorReviews.VendorID);
            return View(vendorReviews);
        }

        // POST: VendorReviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserID,VendorID,Message,Rating,date")] VendorReviews vendorReviews)
        {
            if (id != vendorReviews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorReviews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorReviewsExists(vendorReviews.Id))
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
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", vendorReviews.UserID);
            ViewData["VendorID"] = new SelectList(_context.Vendors, "Id", "Address", vendorReviews.VendorID);
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

        private bool VendorReviewsExists(int id)
        {
            return _context.VendorReviews.Any(e => e.Id == id);
        }
    }
}
