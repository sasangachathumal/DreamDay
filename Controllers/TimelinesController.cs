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
    [Authorize(Roles = "Client, Planner")]
    public class TimelinesController : Controller
    {
        private readonly ITimelineService _timelineService;

        public TimelinesController(ITimelineService timelineService)
        {
            _timelineService = timelineService;
        }

        // GET: Timelines
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
            var timelines = _timelineService.GetTimelinesByWeddingId(_WeddingId);
            return View(timelines);
        }

        // GET: Timelines/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var timeline = _timelineService.GetTimelineById(id);
            if (timeline == null)
            {
                return NotFound();
            }

            return View(timeline);
        }

        // GET: Timelines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Timelines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Location,StartTime,EndTime")] Timeline timeline)
        {
            ModelState.Remove("Wedding");
            if (ModelState.IsValid)
            {
                int? weddingId = HttpContext.Session.GetInt32("WeddingId");
                if (weddingId.HasValue)
                {
                    timeline.WeddingId = weddingId.Value; // Set the WeddingId from session
                    timeline.IsDone = false; // Default value for IsDone
                    _timelineService.AddTimeline(timeline);
                    return RedirectToAction(nameof(Index), new { weddingId = weddingId });
                }
                else
                {
                    ModelState.AddModelError("", "Wedding ID is not set in session.");
                }
            }
            else
            {
                return View(timeline);
            }
            return View(timeline);
        }

        // GET: Timelines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeline = _timelineService.GetTimelineById(id.Value);
            if (timeline == null)
            {
                return NotFound();
            }
            return View(timeline);
        }

        // POST: Timelines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WeddingId,Title,Description,Location,StartTime,EndTime,IsDone")] Timeline timeline)
        {
            if (id != timeline.Id)
            {
                return NotFound();
            }
            ModelState.Remove("Wedding");
            if (ModelState.IsValid)
            {
                int? weddingId = HttpContext.Session.GetInt32("WeddingId");
                if (weddingId.HasValue)
                {
                    timeline.WeddingId = weddingId.Value; // Set the WeddingId from session
                    _timelineService.UpdateTimeline(timeline);
                    return RedirectToAction(nameof(Index), new { weddingId = weddingId });
                }
                else
                {
                    ModelState.AddModelError("", "Wedding ID is not set in session.");
                }
            }
            else
            {
                return View(timeline);
            }
            return View(timeline);
        }

        // GET: Timelines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeline = _timelineService.GetTimelineById(id.Value);
            if (timeline == null)
            {
                return NotFound();
            }

            return View(timeline);
        }

        // POST: Timelines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _timelineService.DeleteTimeline(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
