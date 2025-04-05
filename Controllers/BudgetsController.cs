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
using DreamDay.Business.Service;

namespace DreamDay.Controllers
{
    public class BudgetsController : Controller
    {
        private readonly IBudgetService _budgetService;

        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        // GET: Budgets
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
            var budgets = _budgetService.GetBudgetsByWeddingId(_WeddingId);
            return View(budgets);
        }

        // GET: Budgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = _budgetService.GetBudgetById(id.Value);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // GET: Budgets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,AllocatedAmount,SpendAmount")] Budget budget)
        {
            ModelState.Remove("Wedding");
            if (ModelState.IsValid)
            {
                int? weddingId = HttpContext.Session.GetInt32("WeddingId");
                if (weddingId.HasValue)
                {
                    budget.WeddingId = weddingId.Value; // Set the WeddingId from session
                    _budgetService.AddBudget(budget);
                    return RedirectToAction(nameof(Index), new { weddingId = weddingId });
                }
                else
                {
                    ModelState.AddModelError("", "Wedding ID is not set in session.");
                }
            }
            else
            {
                return View(budget);
            }
            return View(budget);
        }

        // GET: Budgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = _budgetService.GetBudgetById(id.Value);
            if (budget == null)
            {
                return NotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,AllocatedAmount,SpendAmount")] Budget budget)
        {
            if (id != budget.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Wedding");
            if (ModelState.IsValid)
            {
                int? weddingId = HttpContext.Session.GetInt32("WeddingId");
                if (weddingId.HasValue)
                {
                    budget.WeddingId = weddingId.Value; // Set the WeddingId from session
                    _budgetService.UpdateBudget(budget);
                    return RedirectToAction(nameof(Index), new { weddingId = weddingId });
                }
                else
                {
                    ModelState.AddModelError("", "Wedding ID is not set in session.");
                }
            }
            else
            {
                return View(budget);
            }
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budget = _budgetService.GetBudgetById(id.Value);
            if (budget == null)
            {
                return NotFound();
            }

            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _budgetService.DeleteBudget(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
