﻿using System;
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
    public class ChecklistItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IChecklistItemService _checklistItemService;

        public ChecklistItemsController(ApplicationDbContext context, IChecklistItemService checklistItemService)
        {
            _checklistItemService = checklistItemService;
            _context = context;
        }

        // GET: ChecklistItems
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
            var checklistItems = _checklistItemService.GetChecklistItemsByWeddingId(_WeddingId);
            return View(checklistItems);
        }

        // GET: ChecklistItems/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var checklistItem = _checklistItemService.GetChecklistItemById(id);
            if (checklistItem == null)
            {
                return NotFound();
            }

            return View(checklistItem);
        }

        // GET: ChecklistItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChecklistItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DateTime")] ChecklistItem checklistItem)
        {
            ModelState.Remove("Wedding");
            if (ModelState.IsValid)
            {
                int? weddingId = HttpContext.Session.GetInt32("WeddingId");
                if (weddingId.HasValue)
                {
                    checklistItem.WeddingId = weddingId.Value; // Set the WeddingId from session
                    checklistItem.IsDone = false; // Default value for IsDone
                    _checklistItemService.AddChecklistItem(checklistItem);
                    return RedirectToAction(nameof(Index), new { weddingId = weddingId });
                }
                else
                {
                    ModelState.AddModelError("", "Wedding ID is not set in session.");
                }
            }
            else
            {
                ModelState.AddModelError("DateTime", "Date and time must be in the future.");
                return View(checklistItem);
            }
            return View(checklistItem);
        }

        // GET: ChecklistItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistItem = await _context.ChecklistItems.FindAsync(id);
            if (checklistItem == null)
            {
                return NotFound();
            }
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "Id", "ClientId", checklistItem.WeddingId);
            return View(checklistItem);
        }

        // POST: ChecklistItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeddingId,Title,Description,DateTime,IsDone")] ChecklistItem checklistItem)
        {
            if (id != checklistItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checklistItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistItemExists(checklistItem.Id))
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
            ViewData["WeddingId"] = new SelectList(_context.Weddings, "Id", "ClientId", checklistItem.WeddingId);
            return View(checklistItem);
        }

        // GET: ChecklistItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistItem = await _context.ChecklistItems
                .Include(c => c.Wedding)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (checklistItem == null)
            {
                return NotFound();
            }

            return View(checklistItem);
        }

        // POST: ChecklistItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checklistItem = await _context.ChecklistItems.FindAsync(id);
            if (checklistItem != null)
            {
                _context.ChecklistItems.Remove(checklistItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistItemExists(int id)
        {
            return _context.ChecklistItems.Any(e => e.Id == id);
        }
    }
}
