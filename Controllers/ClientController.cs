﻿using DreamDay.Business.Interface;
using DreamDay.Business.Service;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IWeddingService _weddingService;
        private readonly IChecklistItemService _checklistItemService;
        private readonly IGuestService _guestService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITimelineService _timelineService;

        public ClientController(
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
            var wedding = _weddingService.GetWeddingByClientId(SignedInUser?.Id);

            if (wedding != null)
            {
                HttpContext.Session.SetInt32("WeddingId", wedding.Id);
                wedding.ChecklistItems = _checklistItemService.GetChecklistItemsByWeddingId(wedding.Id);
                wedding.Guests = _guestService.GetGuestsByWeddingId(wedding.Id);
                wedding.Timelines = _timelineService.GetTimelinesByWeddingId(wedding.Id);
            }

            return View(wedding);
        }

        public IActionResult MarkAsDone(int itemId)
        {
            if (_checklistItemService.MarkAsDone(itemId))
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public IActionResult MarkAsAttending(int itemId)
        {
            if (_guestService.MarkAsAttending(itemId))
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public IActionResult MarkRimelineAsDone(int itemId)
        {
            if (_timelineService.MarkAsDone(itemId))
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }
    }
}
