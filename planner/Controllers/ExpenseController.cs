using Microsoft.AspNetCore.Mvc;

namespace planner.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
