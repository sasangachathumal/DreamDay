using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.AspNetCore.Authorization;
using DreamDay.Business.Interface;
using DreamDay.Business.Service;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Client")]
    public class GuestsController : Controller
    {
        private readonly IGuestService _guestService;

        public GuestsController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        // GET: Guests
        public async Task<IActionResult> Index(int weddingId)
        {
            int _WeddingId = 0;
            if (weddingId == 0)
            {
                var sessionWeddingId = HttpContext.Session.GetInt32("WeddingId");
                if (sessionWeddingId.HasValue)
                {
                    _WeddingId = sessionWeddingId.Value;
                }
                else
                {
                    _WeddingId = 0;
                }
            }
            else
            {
                _WeddingId = weddingId;
                HttpContext.Session.SetInt32("WeddingId", weddingId);
            }
            var guests = _guestService.GetGuestsByWeddingId(_WeddingId);
            return View(guests);
        }

        // GET: Guests/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var guest = _guestService.GetGuestById(id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // GET: Guests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Guests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Phone,MealPreference,TableNumber")] Guest guest)
        {
            ModelState.Remove("Wedding");
            if (ModelState.IsValid)
            {
                int? weddingId = HttpContext.Session.GetInt32("WeddingId");
                if (weddingId.HasValue)
                {
                    guest.WeddingId = weddingId.Value; // Set the WeddingId from session
                    guest.RSVP = false; // Default value for RSVP
                    _guestService.AddGuest(guest);
                    return RedirectToAction(nameof(Index), new { weddingId = weddingId });
                }
                else
                {
                    ModelState.AddModelError("", "Wedding ID is not set in session.");
                }
            }
            else
            {
                return View(guest);
            }
            return View(guest);
        }

        // GET: Guests/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var guest = _guestService.GetGuestById(id);
            if (guest == null)
            {
                return NotFound();
            }
            return View(guest);
        }

        // POST: Guests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Phone,RSVP,MealPreference,TableNumber")] Guest guest)
        {
            if (id != guest.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Wedding");
            if (ModelState.IsValid)
            {
                int? weddingId = HttpContext.Session.GetInt32("WeddingId");
                if (weddingId.HasValue)
                {
                    guest.WeddingId = weddingId.Value; // Set the WeddingId from session
                    _guestService.UpdateGuest(guest);
                    return RedirectToAction(nameof(Index), new { weddingId = weddingId });
                }
                else
                {
                    ModelState.AddModelError("", "Wedding ID is not set in session.");
                }
            }
            else
            {
                return View(guest);
            }
            return View(guest);
        }

        // GET: Guests/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var guest = _guestService.GetGuestById(id);
            if (guest == null)
            {
                return NotFound();
            }

            return View(guest);
        }

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _guestService.DeleteGuest(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
