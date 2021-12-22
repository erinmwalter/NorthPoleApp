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
        EmployeeTaskDAL etDB = new EmployeeTaskDAL();
        GiftDAL gDB = new GiftDAL();
        LetterDAL lDB = new LetterDAL();

        public IActionResult Index()
        {
            List<Models.Task> tasks = tDB.GetTasks();
            ViewData["gifts"] = gDB.GetGifts();
            return View(tasks);
        }

        public IActionResult CreateTask(Letter l) 
        {
            ViewData["Employees"] = eDB.GetEmployees();
            Models.Task t = new Models.Task(l.GiftId, l.LetterId);
            return View(t);
        }

        [HttpPost]
        public IActionResult CreateTask(Models.Task t, int[] employees) 
        {
            tDB.CreateTask(t);
            int taskId = tDB.GetId(t.LetterId);
            foreach (int i in employees) 
            {
                etDB.CreateEmployeeTask(i, taskId);
            }
            return RedirectToAction("Index", "Task");
        }

        public IActionResult UpdateTask(int id) 
        {
            Models.Task t = tDB.GetTask(id);
            ViewData["employees"] = eDB.GetEmployees();
            return View(t);
        }

        [HttpPost]
        public IActionResult UpdateTask(Models.Task t, int[] employees) 
        {
            //first need to scrub employeestasks of old assignments
            etDB.DeleteByTaskId(t.Id);
            //next need to update task in tasks
            tDB.UpdateTask(t);
            //next need to add in new employeestasks values
            foreach (int i in employees) 
            {
                etDB.CreateEmployeeTask(i, t.Id);
            }

            return RedirectToAction("Index", "Task");
        }

        public IActionResult Delete(int id) 
        {
            Models.Task t = tDB.GetTask(id);
            //first need to delete out of employeestask
            etDB.DeleteByTaskId(t.Id);
            //this deletes both task and associated letter
            tDB.DeleteTask(t);
            return RedirectToAction("Index", "Task");
        }

        public IActionResult TaskList() 
        {
            Employee e = eDB.GetEmployee(EmployeeDAL.CurrentEmployeeId);

            ViewData["gifts"] = gDB.GetGifts();

            return View(e);
        }
    }
}
