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
            if (success)
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ViewBag.Message = "Error: invalid username or password. Try Again.";
                return View(e);
            }
        }

        public IActionResult CreateEmployee()
        {
            Employee e = new Employee();
            return View(e);
        }

        [HttpPost]
        public IActionResult CreateEmployee(Employee e) 
        {
            if (ModelState.IsValid)
            {
                e.Password = "password";
                eDB.CreateEmployee(e);
                return RedirectToAction("Confirmation", "Employee", e);
            }
            else 
            {
                return View(e);
            }
        
        }

        public IActionResult Confirmation(Employee e) 
        {
            return View(e);
        }

        public IActionResult LogOut() 
        {
            eDB.Logout();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult EditEmployee() 
        {
            Employee e = eDB.GetEmployee(EmployeeDAL.CurrentEmployeeId);
            return View(e);
        }

        [HttpPost]
        public IActionResult EditEmployee(Employee e) 
        {
            eDB.UpdateEmployee(e);
            return RedirectToAction("Index", "Employee");
        }

        public IActionResult ViewRoster() 
        {
            List<Employee> employees = eDB.GetEmployees();
            return View(employees);
        }
    }
}
