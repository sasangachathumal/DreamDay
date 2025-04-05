using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DreamDay.Data;
using DreamDay.Models;
using DreamDay.Business.Service;
using Microsoft.AspNetCore.Authorization;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Client, Planner, Admin")]
    public class WeddingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly WeddingService _weddingService;

        public WeddingsController(ApplicationDbContext context, WeddingService weddingService)
        {
            _context = context;
            _weddingService = weddingService;
        }

        // GET: Weddings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Weddings.Include(w => w.Client).Include(w => w.Planner);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Weddings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wedding = await _context.Weddings
                .Include(w => w.Client)
                .Include(w => w.Planner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wedding == null)
            {
                return NotFound();
            }

            return View(wedding);
        }

        // GET: Weddings/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["PlannerId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Weddings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,PlannerId,WeddingDate,TotalGuests,FullBudget")] Wedding wedding)
        {
            if (ModelState.IsValid)
            {
                var result = _weddingService.AddWedding(wedding);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {

                }
            }
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", wedding.ClientId);
            ViewData["PlannerId"] = new SelectList(_context.Users, "Id", "Id", wedding.PlannerId);
            return View(wedding);
        }

        // GET: Weddings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wedding = await _context.Weddings.FindAsync(id);
            if (wedding == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", wedding.ClientId);
            ViewData["PlannerId"] = new SelectList(_context.Users, "Id", "Id", wedding.PlannerId);
            return View(wedding);
        }

        // POST: Weddings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClientId,PlannerId,WeddingDate,TotalGuests,FullBudget")] Wedding wedding)
        {
            if (id != wedding.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wedding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WeddingExists(wedding.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Users, "Id", "Id", wedding.ClientId);
            ViewData["PlannerId"] = new SelectList(_context.Users, "Id", "Id", wedding.PlannerId);
            return View(wedding);
        }

        // GET: Weddings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wedding = await _context.Weddings
                .Include(w => w.Client)
                .Include(w => w.Planner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wedding == null)
            {
                return NotFound();
            }

            return View(wedding);
        }

        // POST: Weddings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wedding = await _context.Weddings.FindAsync(id);
            if (wedding != null)
            {
                _context.Weddings.Remove(wedding);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WeddingExists(int id)
        {
            return _context.Weddings.Any(e => e.Id == id);
        }
    }
}
