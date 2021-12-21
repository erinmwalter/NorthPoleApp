using Microsoft.AspNetCore.Mvc;
using NorthPoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthPoleApp.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDAL eDB = new EmployeeDAL();

        public IActionResult Index()
        {
            Employee e = eDB.GetEmployee(EmployeeDAL.CurrentEmployeeId);
            return View(e);
        }

        public IActionResult Admin()
        {
            Employee e = eDB.GetEmployee(EmployeeDAL.CurrentEmployeeId);
            return View(e);
        }

        public IActionResult Login() 
        {
            ViewBag.Message = "";
            Employee e = new Employee();
            return View(e);
        }

        [HttpPost]
        public IActionResult Login(Employee e) 
        {
            bool success = eDB.isSuccess(e);
            if (success && EmployeeDAL.IsAdmin)
            {

                return RedirectToAction("Admin", "Employee");
            }
            else if (success) 
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ViewBag.Message = "Error: invalid username or password. Try Again.";
                return View(e);
            }
        }
    }
}
