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
        //private readonly IVendorService _vendorService;

        public VendorPackageBookingsController(
            ApplicationDbContext context, 
            IVendorPackageBookingService vendorPackageBookingService,
            IWeddingService weddingService,
            IVendorPackageService vendorPackageService)
        {
            _context = context;
            _vendorPackageBookingService = vendorPackageBookingService;
            _weddingService = weddingService;
            _vendorPackageService = vendorPackageService;
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
                }
            }
            return View(packageBookings);
        }

        // GET: VendorPackageBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorPackageBooking = await _context.VendorPackagesBooking
                .Include(v => v.VendorPackage)
                .Include(v => v.Wedding)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorPackageBooking == null)
            {
                return NotFound();
            }

            return View(vendorPackageBooking);
        }

        // GET: VendorPackageBookings/Create
        public IActionResult Create()
        {
            ViewData["VendorPackageID"] = new SelectList(_context.VendorPackages, "Id", "Name");
            ViewData["WeddingID"] = new SelectList(_context.Weddings, "Id", "ClientId");
            return View();
        }

        // POST: VendorPackageBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WeddingID,VendorPackageID,BookDate,IsConfirmed")] VendorPackageBooking vendorPackageBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendorPackageBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorPackageID"] = new SelectList(_context.VendorPackages, "Id", "Name", vendorPackageBooking.VendorPackageID);
            ViewData["WeddingID"] = new SelectList(_context.Weddings, "Id", "ClientId", vendorPackageBooking.WeddingID);
            return View(vendorPackageBooking);
        }

        // GET: VendorPackageBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorPackageBooking = await _context.VendorPackagesBooking.FindAsync(id);
            if (vendorPackageBooking == null)
            {
                return NotFound();
            }
            ViewData["VendorPackageID"] = new SelectList(_context.VendorPackages, "Id", "Name", vendorPackageBooking.VendorPackageID);
            ViewData["WeddingID"] = new SelectList(_context.Weddings, "Id", "ClientId", vendorPackageBooking.WeddingID);
            return View(vendorPackageBooking);
        }

        // POST: VendorPackageBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeddingID,VendorPackageID,BookDate,IsConfirmed")] VendorPackageBooking vendorPackageBooking)
        {
            if (id != vendorPackageBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendorPackageBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorPackageBookingExists(vendorPackageBooking.Id))
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
            ViewData["VendorPackageID"] = new SelectList(_context.VendorPackages, "Id", "Name", vendorPackageBooking.VendorPackageID);
            ViewData["WeddingID"] = new SelectList(_context.Weddings, "Id", "ClientId", vendorPackageBooking.WeddingID);
            return View(vendorPackageBooking);
        }

        // GET: VendorPackageBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorPackageBooking = await _context.VendorPackagesBooking
                .Include(v => v.VendorPackage)
                .Include(v => v.Wedding)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendorPackageBooking == null)
            {
                return NotFound();
            }

            return View(vendorPackageBooking);
        }

        // POST: VendorPackageBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorPackageBooking = await _context.VendorPackagesBooking.FindAsync(id);
            if (vendorPackageBooking != null)
            {
                _context.VendorPackagesBooking.Remove(vendorPackageBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorPackageBookingExists(int id)
        {
            return _context.VendorPackagesBooking.Any(e => e.Id == id);
        }
    }
}
