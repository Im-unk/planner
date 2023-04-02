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

            var tasks = new Tasks();
            var expense = new Expense();
            var description = new Description();
            var goals = new Goals();
            var year = new List<Year>();
            var month = new List<Month>()
            {
                new Month {MonthId = 1 , MonthName = "Farvardin" , MonthNumber = 1},
                new Month {MonthId = 2 , MonthName = "Ordibehesht" , MonthNumber = 2},
                new Month {MonthId = 3 , MonthName = "Khordad" , MonthNumber = 3}
            };
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
            var year = new List<Year>();
            var month = new List<Month>()
            {
                new Month {MonthId = 1 , MonthName = "Farvardin" , MonthNumber = 1},
                new Month {MonthId = 2 , MonthName = "Ordibehesht" , MonthNumber = 2},
                new Month {MonthId = 3 , MonthName = "Khordad" , MonthNumber = 3}
            };
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

            return Content("Year" + YearId + " - " + "Month" + MonthId);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}