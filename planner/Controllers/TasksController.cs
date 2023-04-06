using Microsoft.AspNetCore.Mvc;
using planner.Models;
using MySql.Data.MySqlClient;
using planner.ViewModels;
using System.Diagnostics;

namespace planner.Controllers
{
    public class TasksController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        
    }
}
