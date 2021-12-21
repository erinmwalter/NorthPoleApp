using Microsoft.AspNetCore.Mvc;
using NorthPoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthPoleApp.Controllers
{
    public class LetterController : Controller
    {
        LetterDAL lDB = new LetterDAL();
        GiftDAL gDB = new GiftDAL();

        public IActionResult Index()
        {
            Letter l = new Letter();
            ViewData["Gifts"] = gDB.GetGifts();
            ViewBag.Message = "";
            return View(l);
        }

        [HttpPost]
        public IActionResult Index(Letter l) 
        {
            if (ModelState.IsValid) 
            {
                lDB.CreateLetter(l);
                return RedirectToAction("Confirmation", "Letter", l);
            }
            else
            {
                ViewBag.Message = "Please Fix Errors Below and Resubmit.";
                return View(l);
            }
        }

        public IActionResult Confirmation(Letter l) 
        {
            return View(l);
        }
    }
}
