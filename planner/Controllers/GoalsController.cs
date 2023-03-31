using Microsoft.AspNetCore.Mvc;

namespace planner.Controllers
{
    public class GoalsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
