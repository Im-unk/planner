﻿using Microsoft.AspNetCore.Mvc;

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
