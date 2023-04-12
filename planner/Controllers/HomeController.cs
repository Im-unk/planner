using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using planner.Models;
using planner.ViewModels;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Threading.Tasks;
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
                Expenses = expense,
                Description = description,
                Goals = goals,
                Year = year,
                Month = month,
                Users = users
            };

            return View(_home);
        }

        public IActionResult Activity(int? YearId , int? MonthId , int? DayId )
        {
            if (!YearId.HasValue)
                YearId = 1402;

            if (!MonthId.HasValue)
                MonthId = 1;

            var tasks = new Tasks();
            List<Tasks> tasksList = new List<Tasks>();
            var expense = new Expense();
            List<Expense> expenseList = new List<Expense>();
            var description = new Description();
            List<Description> descriptionList = new List<Description>();
            var goals = new Goals();
            List<Goals> goalsList = new List<Goals>();
            var year = new List<Year>();
            var month = new List<Month>();
            var users = new Users();

            var _home = new HomeViewModels
            {
                Tasks = tasks,
                TasksList = tasksList,
                Expenses = expense,
                ExpenseList = expenseList,
                Description = description,
                DescriptionList = descriptionList,
                Goals = goals,
                GoalsList = goalsList,
                Year = year,
                Month = month,
                Users = users
            };

            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string queryTask = "SELECT * FROM Tasks where MonthId = @monthId and YearId = @yearId and DayId = @dayId";

                using (MySqlCommand command = new MySqlCommand(queryTask, connection))
                {
                    command.Parameters.AddWithValue("@monthId", MonthId);
                    command.Parameters.AddWithValue("@yearId", YearId);
                    command.Parameters.AddWithValue("@dayId", DayId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tasks task = new Tasks();
                            task.TaskId = reader.GetInt32("TaskId");
                            task.IsDone = reader.GetInt32("IsDone");
                            task.TaskName = reader.GetString("TaskName");
                            task.MonthId = reader.GetInt32("MonthId");
                            task.YearId = reader.GetInt32("YearId");
                            task.DayId = reader.GetInt32("DayId");

                            tasksList.Add(task);

                        }
                    }
                }

                string queryExpense = "SELECT * FROM Expense where MonthId = @monthId and YearId = @yearId and DayId = @dayId";

                using (MySqlCommand command = new MySqlCommand(queryExpense, connection))
                {
                    command.Parameters.AddWithValue("@monthId", MonthId);
                    command.Parameters.AddWithValue("@yearId", YearId);
                    command.Parameters.AddWithValue("@dayId", DayId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Expense expenses = new Expense();
                            expenses.ExpenseId = reader.GetInt32("ExpenseId");
                            expenses.ExpenseTitle = reader.GetString("ExpenseTitle");
                            expenses.ExpensePrice = reader.GetString("ExpensePrice");
                            expenses.MonthId = reader.GetInt32("MonthId");
                            expenses.YearId = reader.GetInt32("YearId");
                            expenses.DayId = reader.GetInt32("DayId");

                            expenseList.Add(expenses);

                        }
                    }
                }


                string queryGoal = "SELECT * FROM Goals where MonthId = @monthId and YearId = @yearId ";

                using (MySqlCommand command = new MySqlCommand(queryGoal, connection))
                {
                    command.Parameters.AddWithValue("@monthId", MonthId);
                    command.Parameters.AddWithValue("@yearId", YearId);
                  

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Goals goal = new Goals();
                            goal.GoalId = reader.GetInt32("GoalId");
                            goal.GoalName = reader.GetString("GoalName");                          
                            goal.MonthId = reader.GetInt32("MonthId");
                            goal.YearId = reader.GetInt32("YearId");

                            goalsList.Add(goal);

                        }
                    }
                }

                string queryDescription = "SELECT * FROM Description where MonthId = @monthId and YearId = @yearId and DayId = @dayId";

                using (MySqlCommand command = new MySqlCommand(queryDescription, connection))
                {
                    command.Parameters.AddWithValue("@monthId", MonthId);
                    command.Parameters.AddWithValue("@yearId", YearId);
                    command.Parameters.AddWithValue("@dayId", DayId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Description desc = new Description();
                            desc.DescriptionId = reader.GetInt32("DescriptionId");
                            desc.DescriptionName = reader.GetString("DescriptionName");
                            desc.MonthId = reader.GetInt32("MonthId");
                            desc.YearId = reader.GetInt32("YearId");
                            desc.DayId = reader.GetInt32("DayId");

                            descriptionList.Add(desc);

                        }
                    }
                }

            }

            ViewBag.MonthId = MonthId;

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

                    // get the query strings in a string 
                    string _queryParams = "?YearId=" + task.Tasks.YearId + "&MonthId=" + task.Tasks.MonthId + "&DayId=" + task.Tasks.DayId;
                    // redirect it to the URL
                    return Redirect("/Home/Activity" + _queryParams);
                }

            }

        }

        [HttpPost]
        public IActionResult DeleteTask(int _id)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // create a new ADO.NET connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                // open the connection
                connection.Open();

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand("DELETE from Tasks where TaskId = @TaskId", connection))
                {
                    command.Parameters.AddWithValue("@TaskId", _id);
                    
                    //execute the command
                    command.ExecuteNonQuery();

                    // returns inserted task as json
                    return Json(new {success = true});
                }

            }

        }

        [HttpPost]
        public IActionResult UpdateTask(int _id , int _isDone)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // create a new ADO.NET connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                // open the connection
                connection.Open();

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand("Update Tasks set isDone = @isDone where TaskId = @TaskId", connection))
                {
                    command.Parameters.AddWithValue("@TaskId", _id);
                    command.Parameters.AddWithValue("@isDone", _isDone);

                    //execute the command
                    command.ExecuteNonQuery();

                    // returns inserted task as json
                    return Json(new { success = true });
                }

            }

        }


        [HttpPost]
        public IActionResult AddExpense(HomeViewModels expense)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // create a new ADO.NET connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                // open the connection
                connection.Open();

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Expense (ExpenseTitle, ExpensePrice, MonthId, YearId, DayId) VALUES (@ExpenseTitle , @ExpensePrice , @MonthId , @YearId , @DayId)", connection))
                {
                    command.Parameters.AddWithValue("@ExpenseTitle", expense.Expenses.ExpenseTitle);
                    command.Parameters.AddWithValue("@ExpensePrice", expense.Expenses.ExpensePrice);
                    command.Parameters.AddWithValue("@MonthId", expense.Expenses.MonthId);
                    command.Parameters.AddWithValue("@YearId", expense.Expenses.YearId);
                    command.Parameters.AddWithValue("@DayId", expense.Expenses.DayId);

                    //execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // get the query strings in a string 
                    string _queryParams = "?YearId=" + expense.Expenses.YearId + "&MonthId=" + expense.Expenses.MonthId + "&DayId=" + expense.Expenses.DayId;
                    // redirect it to the URL
                    return Redirect("/Home/Activity" + _queryParams);
                }

            }

        }

        [HttpPost]
        public IActionResult DeleteExpense(int _id)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // create a new ADO.NET connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                // open the connection
                connection.Open();

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand("DELETE from Expense where ExpenseId = @ExpenseId", connection))
                {
                    command.Parameters.AddWithValue("@ExpenseId", _id);

                    //execute the command
                    command.ExecuteNonQuery();

                    // returns inserted task as json
                    return Json(new { success = true });
                }

            }

        }

        [HttpPost]
        public IActionResult AddGoal(HomeViewModels goal)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // create a new ADO.NET connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                // open the connection
                connection.Open();

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Goals (GoalName, MonthId, YearId) VALUES (@GoalName , @MonthId , @YearId)", connection))
                {
                    command.Parameters.AddWithValue("@GoalName", goal.Goals.GoalName);
                    command.Parameters.AddWithValue("@MonthId", goal.Goals.MonthId);
                    command.Parameters.AddWithValue("@YearId", goal.Goals.YearId);

                    //execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // get the query strings in a string 
                    string _queryParams = "?YearId=" + goal.Goals.YearId + "&MonthId=" + goal.Goals.MonthId + "&DayId=" + goal.Goals.DayId;
                    // redirect it to the URL
                    return Redirect("/Home/Activity" + _queryParams);
                }

            }

        }

        [HttpPost]
        public IActionResult DeleteGoal(int _id)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // create a new ADO.NET connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                // open the connection
                connection.Open();

                // sql command to delete the data 
                using (MySqlCommand command = new MySqlCommand("DELETE from Goals where GoalId = @GoalId", connection))
                {
                    command.Parameters.AddWithValue("@GoalId", _id);

                    //execute the command
                    command.ExecuteNonQuery();

                    // returns inserted task as json
                    return Json(new { success = true });
                }

            }

        }

        [HttpPost]
        public IActionResult AddDescription(string _text, int _YearId , int _MonthId , int _DayId)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // create a new ADO.NET connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                // open the connection
                connection.Open();

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Description (DescriptionName, MonthId, YearId, DayId) VALUES (@DescriptionName , @MonthId , @YearId , @DayId)", connection))
                {
                    command.Parameters.AddWithValue("@DescriptionName", _text);
                    command.Parameters.AddWithValue("@MonthId", _MonthId);
                    command.Parameters.AddWithValue("@YearId", _YearId);
                    command.Parameters.AddWithValue("@DayId", _DayId);

                    //execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    // get the query strings in a string 
                    string _queryParams = "?YearId=" + _YearId + "&MonthId=" + _MonthId + "&DayId=" + _DayId;
                    // redirect it to the URL
                    return Redirect("/Home/Activity" + _queryParams);
                }

            }

        }

    }
}