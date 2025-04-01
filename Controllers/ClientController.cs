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
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ClientController(IWeddingService weddingService, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _weddingService = weddingService;
        }
        public IActionResult Dashboard()
        {
            var SignedInUser = _signInManager.UserManager.GetUserAsync(User).Result;
            var weddings = _weddingService.GetWeddingByClientId(SignedInUser?.Id).Result;
            return View(weddings);
        }
    }
}
