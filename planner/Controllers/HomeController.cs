using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Tls;
using planner.Models;
using planner.ViewModels;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace planner.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
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
            var month = new List<Month>();
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

        public IActionResult ForgetPassword()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult Activity(int? YearId, int? MonthId, int? DayId)
        {
            if (!YearId.HasValue)
                YearId = 1402;

            if (!MonthId.HasValue)
                MonthId = 1;

            string userId = Request.Cookies["Id"];

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

                string queryTask = "SELECT * FROM Tasks where MonthId = @monthId and YearId = @yearId and DayId = @dayId and Id = @Id";

                using (MySqlCommand command = new MySqlCommand(queryTask, connection))
                {
                    command.Parameters.AddWithValue("@monthId", MonthId);
                    command.Parameters.AddWithValue("@yearId", YearId);
                    command.Parameters.AddWithValue("@dayId", DayId);
                    command.Parameters.AddWithValue("@Id", userId);

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
                            task.Id = reader.GetInt32("Id");
                            tasksList.Add(task);

                        }
                    }
                }

                string queryExpense = "SELECT * FROM Expense where MonthId = @monthId and YearId = @yearId and DayId = @dayId and Id = @Id";

                using (MySqlCommand command = new MySqlCommand(queryExpense, connection))
                {
                    command.Parameters.AddWithValue("@monthId", MonthId);
                    command.Parameters.AddWithValue("@yearId", YearId);
                    command.Parameters.AddWithValue("@dayId", DayId);
                    command.Parameters.AddWithValue("@Id", userId);

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
                            expenses.Id = reader.GetInt32("Id");
                            expenseList.Add(expenses);

                        }
                    }
                }


                string queryGoal = "SELECT * FROM Goals where MonthId = @monthId and YearId = @yearId and Id = @Id ";

                using (MySqlCommand command = new MySqlCommand(queryGoal, connection))
                {
                    command.Parameters.AddWithValue("@monthId", MonthId);
                    command.Parameters.AddWithValue("@yearId", YearId);
                    command.Parameters.AddWithValue("@Id", userId);


                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Goals goal = new Goals();
                            goal.GoalId = reader.GetInt32("GoalId");
                            goal.GoalName = reader.GetString("GoalName");
                            goal.MonthId = reader.GetInt32("MonthId");
                            goal.YearId = reader.GetInt32("YearId");
                            goal.Id = reader.GetInt32("Id");
                            goalsList.Add(goal);

                        }
                        
                    }
                }

                string queryDescription = "SELECT * FROM Description where MonthId = @monthId and YearId = @yearId and DayId = @dayId and Id = @Id ";

                using (MySqlCommand command = new MySqlCommand(queryDescription, connection))
                {
                    command.Parameters.AddWithValue("@monthId", MonthId);
                    command.Parameters.AddWithValue("@yearId", YearId);
                    command.Parameters.AddWithValue("@dayId", DayId);
                    command.Parameters.AddWithValue("@Id", userId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Description desc = new Description();
                            desc.DescriptionId = reader.GetInt32("DescriptionId");
                            if (!reader.IsDBNull(reader.GetOrdinal("DescriptionName")))
                            {
                                desc.DescriptionName = reader.GetString("DescriptionName");
                            }
                            desc.MonthId = reader.GetInt32("MonthId");
                            desc.YearId = reader.GetInt32("YearId");
                            desc.DayId = reader.GetInt32("DayId");
                            desc.Id = reader.GetInt32("Id");

                            descriptionList.Add(desc);

                        }            
                    }
                }

                connection.Close();
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
            string userId = Request.Cookies["Id"];

            if (Convert.ToInt32(userId) != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();

            }
        }

        public IActionResult Logout()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return RedirectToAction("Login" , "Home");
        }

        public IActionResult HeaderName()
        {
            int userId = int.Parse(Request.Cookies["Id"]);

            string connectionString = _config.GetConnectionString("MySqlConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Username FROM Users WHERE Id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", userId);

                    using MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string Username = reader.GetString("Username");
                        var user = new Users();
                        var viewmodel = new HomeViewModels { Users = user };
                        connection.Close();
                        return Content(Username);
                    }

                    return PartialView("_header", null);
                }
            }

        }

        public IActionResult Register()
        {
            string userId = Request.Cookies["Id"];

            if (Convert.ToInt32(userId) != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Users users = new Users();
                return View(users);

            }
        }
        // task section
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
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Tasks (TaskName , IsDone, MonthId , YearId , DayId, Id) VALUES (@TaskName , @IsDone , @MonthId , @YearId , @DayId , @Id)", connection))
                {
                    command.Parameters.AddWithValue("@TaskName", task.Tasks.TaskName);
                    command.Parameters.AddWithValue("@IsDone", task.Tasks.IsDone);
                    command.Parameters.AddWithValue("@MonthId", task.Tasks.MonthId);
                    command.Parameters.AddWithValue("@YearId", task.Tasks.YearId);
                    command.Parameters.AddWithValue("@DayId", task.Tasks.DayId);
                    command.Parameters.AddWithValue("@Id", task.Tasks.Id);

                    //execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    connection.Close();

                    // get the query strings in a string 
                    string _queryParams = "?YearId=" + task.Tasks.YearId + "&MonthId=" + task.Tasks.MonthId + "&DayId=" + task.Tasks.DayId;
                    // redirect it to the URL
                    return Redirect("/Home/Activity" + _queryParams);
                }

                ;
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


                    connection.Close();

                    // returns inserted task as json
                    return Json(new { success = true });
                }

                

            }

        }

        [HttpPost]
        public IActionResult UpdateTask(int _id, int _isDone)
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


                    connection.Close();

                    // returns inserted task as json
                    return Json(new { success = true });
                }

            }

        }

        // expense section
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
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Expense (ExpenseTitle, ExpensePrice, MonthId, YearId, DayId, Id) VALUES (@ExpenseTitle , @ExpensePrice , @MonthId , @YearId , @DayId , @Id)", connection))
                {
                    command.Parameters.AddWithValue("@ExpenseTitle", expense.Expenses.ExpenseTitle);
                    command.Parameters.AddWithValue("@ExpensePrice", expense.Expenses.ExpensePrice);
                    command.Parameters.AddWithValue("@MonthId", expense.Expenses.MonthId);
                    command.Parameters.AddWithValue("@YearId", expense.Expenses.YearId);
                    command.Parameters.AddWithValue("@DayId", expense.Expenses.DayId);
                    command.Parameters.AddWithValue("@Id", expense.Expenses.Id);

                    //execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    connection.Close();

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
                    connection.Close();

                    // returns inserted task as json
                    return Json(new { success = true });
                }

                

            }

        }

        //goal section
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
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Goals (GoalName, MonthId, YearId, Id) VALUES (@GoalName , @MonthId , @YearId , @Id)", connection))
                {
                    command.Parameters.AddWithValue("@GoalName", goal.Goals.GoalName);
                    command.Parameters.AddWithValue("@MonthId", goal.Goals.MonthId);
                    command.Parameters.AddWithValue("@YearId", goal.Goals.YearId);
                    command.Parameters.AddWithValue("@Id", goal.Goals.Id);

                    //execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    connection.Close();
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

                    connection.Close();

                    // returns inserted task as json
                    return Json(new { success = true });
                }

                

            }

        }

        //description section
        [HttpPost]
        public IActionResult AddDescription(string _text, int _YearId, int _MonthId, int _DayId, string _Id)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // create a new ADO.NET connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                // open the connection
                connection.Open();

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Description (DescriptionName, MonthId, YearId, DayId, Id) VALUES (@DescriptionName , @MonthId , @YearId , @DayId , @Id)", connection))
                {
                    command.Parameters.AddWithValue("@DescriptionName", _text);
                    command.Parameters.AddWithValue("@MonthId", _MonthId);
                    command.Parameters.AddWithValue("@YearId", _YearId);
                    command.Parameters.AddWithValue("@DayId", _DayId);
                    command.Parameters.AddWithValue("@Id", _Id);

                    //execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    connection.Close();

                    // get the query strings in a string 
                    string _queryParams = "?YearId=" + _YearId + "&MonthId=" + _MonthId + "&DayId=" + _DayId;
                    // redirect it to the URL
                    return Redirect("/Home/Activity" + _queryParams);
                }

                
            }

        }

        [HttpPost]
        public IActionResult UpdateDescription(string _text, int _YearId, int _MonthId, int _DayId, int _Id)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // create a new ADO.NET connection
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                // open the connection
                connection.Open();

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand("UPDATE Description SET DescriptionName = @DescriptionName WHERE MonthId = @MonthId and YearId = @YearId and DayId = @DayId and Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@DescriptionName", _text);
                    command.Parameters.AddWithValue("@MonthId", _MonthId);
                    command.Parameters.AddWithValue("@YearId", _YearId);
                    command.Parameters.AddWithValue("@DayId", _DayId);
                    command.Parameters.AddWithValue("@Id", _Id);

                    //execute the command
                    int rowsAffected = command.ExecuteNonQuery();

                    connection.Close();

                    // get the query strings in a string 
                    string _queryParams = "?YearId=" + _YearId + "&MonthId=" + _MonthId + "&DayId=" + _DayId;
                    // redirect it to the URL
                    return Redirect("/Home/Activity" + _queryParams);
                }

                

            }

        }

        //users section
        [HttpPost]
        public IActionResult Register(Users users)
        {
            //Get the connection string from the configuration file
            string connectionString = _config.GetConnectionString("MySqlConnection");

            // check wether the user exits or not
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // open the connection
                connection.Open();

                string checkUser = "SELECT COUNT(*) FROM Users WHERE mobile = @mobile";

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand(checkUser, connection))
                {
                    command.Parameters.AddWithValue("@mobile", users.Mobile);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count > 0)
                    {
                        ViewBag.ErrorMessage = "کاربر قبلا ایجاد شده است";
                        return View();
                    }

                }

                connection.Close();

            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // open the connection
                connection.Open();

                string createUser = "INSERT INTO Users (Username, Password, Mobile, RegisterDate, LoginDate, Address) VALUES ( @Username, @Password, @Mobile, @RegisterDate, @LoginDate, @Address)";

                // sql command to insert the data 
                using (MySqlCommand command = new MySqlCommand(createUser, connection))
                {
                    command.Parameters.AddWithValue("@Username", users.UserName);
                    command.Parameters.AddWithValue("@Password", users.Password);
                    command.Parameters.AddWithValue("@Mobile", users.Mobile);
                    command.Parameters.AddWithValue("@RegisterDate", users.RegisterDate);
                    command.Parameters.AddWithValue("@LoginDate", users.LoginDate);
                    command.Parameters.AddWithValue("@Address", users.Address);

                    //execute the command
                    command.ExecuteNonQuery();
                }

                connection.Close();

            }

            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7),
                HttpOnly = true
            };

            Response.Cookies.Append("UserId", users.Id.ToString(), cookieOptions);
            Response.Cookies.Append("Username", users.UserName.ToString(), cookieOptions);

            return RedirectToAction("Index");

        }

        public Users GetUser(string mobile, string password)
        {
            string connectionString = _config.GetConnectionString("MySqlConnection");

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string queryUser = "SELECT * FROM Users where Mobile = @Mobile and Password = @Password";

                using (MySqlCommand command = new MySqlCommand(queryUser, connection))
                {
                    command.Parameters.AddWithValue("@Mobile", mobile);
                    command.Parameters.AddWithValue("@Password", password);


                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Users
                            {
                                Id = (int)reader["Id"],
                                UserName = (string)reader["Username"],
                                Password = (string)reader["Password"],
                                Mobile = (string)reader["Mobile"],

                            };
                        }

                        connection.Close();
                    }

                    return null;
                }
  
            }

        }

        [HttpPost]
        public IActionResult Login(string mobile , string password)
        {
            var user = GetUser(mobile, password);

            if (user != null)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(7);

                Response.Cookies.Append("Id", user.Id.ToString(), options);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "نام کاربری یا گذرواژه اشتباه است";
                return View();
            }
        }
    }
    
}