using Microsoft.AspNetCore.Mvc;
using planner.Models;
using planner.ViewModels;
using System.Diagnostics;

namespace planner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Activity(int? YearId , int? MonthId)
        {
            if (!YearId.HasValue)
                YearId = 1402;

            if (!MonthId.HasValue)
                MonthId = 1;

            var tasks = new Tasks();
            var expense = new Expense();
            var description = new Description();
            var goals = new Goals();
            var year = new Year();
            var month = new Month();
            var users = new Users();

            var _home = new HomeViewModels
            {
                Tasks = tasks,
                Expense = expense,
                Description = description,
                Goals = goals,
                Year = year,
                Month = month,
                Users = users
            };

            return View(_home);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}