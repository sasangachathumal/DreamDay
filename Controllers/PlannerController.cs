using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DreamDay.Controllers
{
    [Authorize(Roles = "Planner")]
    public class PlannerController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
