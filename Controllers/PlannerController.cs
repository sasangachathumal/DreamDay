﻿using DreamDay.Business.Interface;
using DreamDay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Planner")]
    public class PlannerController : Controller
    {
        private readonly IWeddingService _weddingService;
        private readonly IChecklistItemService _checklistItemService;
        private readonly IGuestService _guestService;
        private readonly ITimelineService _timelineService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public PlannerController(
            IWeddingService weddingService,
            SignInManager<ApplicationUser> signInManager,
            IChecklistItemService checklistItemService,
            IGuestService guestService,
            ITimelineService timelineService)
        {
            _signInManager = signInManager;
            _weddingService = weddingService;
            _checklistItemService = checklistItemService;
            _guestService = guestService;
            _timelineService = timelineService;
        }
        public IActionResult Dashboard()
        {
            var SignedInUser = _signInManager.UserManager.GetUserAsync(User).Result;
            var weddings = _weddingService.GetWeddingByPlannerId(SignedInUser?.Id);

            if (weddings == null)
            {
                return NotFound();
            }

            if (weddings.Count > 0)
            {
                foreach (var wedding in weddings)
                {
                    wedding.ChecklistItems = _checklistItemService.GetChecklistItemsByWeddingId(wedding.Id);
                    wedding.Guests = _guestService.GetGuestsByWeddingId(wedding.Id);
                    wedding.Timelines = _timelineService.GetTimelinesByWeddingId(wedding.Id);
                }
            }
            return View(weddings);
        }
    }
}
