using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using planner.Models;
using planner.ViewModels;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Web;

namespace planner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger , IConfiguration config)
        {
            _logger = logger;
            _config = config;
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

            return View(_home);
        }

        // getting my connection string from appsetting.json
        private readonly IConfiguration _config;


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

        [HttpPost]
        public IActionResult AddTask(HomeViewModels task)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // create a new ADO.NET connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                
                // open the connection
                connection.Open();

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Tasks (TaskName , IsDone, MonthId , YearId , DayId) VALUES (@TaskName , @IsDone , @MonthId , @YearId , @DayId)", connection))
                {
                    command.Parameters.AddWithValue("@TaskName", task.Tasks.TaskName);
                    command.Parameters.AddWithValue("@IsDone", task.Tasks.IsDone);
                    command.Parameters.AddWithValue("@MonthId", task.Tasks.MonthId);
                    command.Parameters.AddWithValue("@YearId", task.Tasks.YearId);
                    command.Parameters.AddWithValue("@DayId", task.Tasks.DayId);

                    //execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // returns inserted task as json
                    return Json(task);
                }

            }

        }
    }
}