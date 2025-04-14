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
using Microsoft.AspNetCore.Identity;
using DreamDay.Business.Interface;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Client")]
    public class WeddingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWeddingService _weddingService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public WeddingsController(
            ApplicationDbContext context,
            IWeddingService weddingService, 
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager
            )
        {
            _context = context;
            _weddingService = weddingService;
            _signInManager = signInManager;
            _userManager = userManager;
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
            return View();
        }

        // POST: Weddings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WeddingDate,TotalGuests,FullBudget")] Wedding wedding)
        {
            ModelState.Remove("Client");
            ModelState.Remove("Planner");
            ModelState.Remove("ClientId");
            ModelState.Remove("PlannerId");
            if (ModelState.IsValid)
            {
                var SignedInUser = _signInManager.UserManager.GetUserAsync(User).Result;
                wedding.ClientId = SignedInUser.Id;
                var result = _weddingService.AddWedding(wedding);
                if (result)
                {
                    return RedirectToAction("Dashboard", "Client");
                }
                else
                {

                }
            }
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
        public async Task<IActionResult> SelectPlanner()
        {
            var planners = await _userManager.GetUsersInRoleAsync("Planner");
            return View(planners);
        }

        public async Task<IActionResult> AssignPllaner(string id)
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
            _weddingService.AssignPlanner(id, _WeddingId);
            return RedirectToAction("Dashboard", "Client");
        }


        private bool WeddingExists(int id)
        {
            return _context.Weddings.Any(e => e.Id == id);
        }
    }
}
