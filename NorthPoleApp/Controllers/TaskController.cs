using Microsoft.AspNetCore.Mvc;
using NorthPoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthPoleApp.Controllers
{
    public class TaskController : Controller
    {
        EmployeeDAL eDB = new EmployeeDAL();
        TaskDAL tDB = new TaskDAL();

        public IActionResult Index()
        {
            List<Models.Task> tasks = tDB.GetTasks();
            return View();
        }

        public IActionResult CreateTask(Letter l) 
        {
            ViewData["Employees"] = eDB.GetEmployees();
            Models.Task t = new Models.Task(l.GiftId, l.LetterId);
            return View(t);
        }

        [HttpPost]
        public IActionResult CreateTask(Models.Task t) 
        {
            tDB.CreateTask(t);
            return RedirectToAction("Index", "Task");
        }
    }
}
