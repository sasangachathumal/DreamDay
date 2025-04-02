using DreamDay.Business.Interface;
using DreamDay.Business.Service;
using DreamDay.Data;
using DreamDay.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly IWeddingService _weddingService;
        private readonly IChecklistItemService _checklistItemService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ClientController(
            IWeddingService weddingService, 
            SignInManager<ApplicationUser> signInManager,
            IChecklistItemService checklistItemService)
        {
            _signInManager = signInManager;
            _weddingService = weddingService;
            _checklistItemService = checklistItemService;
        }
        public IActionResult Dashboard()
        {
            var SignedInUser = _signInManager.UserManager.GetUserAsync(User).Result;
            var weddings = _weddingService.GetWeddingByClientId(SignedInUser?.Id);

            if (weddings == null)
            {
                return NotFound();
            }

            if (weddings.Count > 0)
            {
                foreach (var wedding in weddings)
                {
                    wedding.ChecklistItems = _checklistItemService.GetChecklistItemsByWeddingId(wedding.Id);
                }
            }

            return View(weddings);
        }

        public IActionResult MarkAsDone(int itemId)
        {
            if (_checklistItemService.MarkAsDone(itemId))
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }
    }
}
