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
        EmployeeDAL eDB = new EmployeeDAL();

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

        public IActionResult LetterList() 
        {
            List<Letter> l = lDB.GetLetters().Where(x => x.IsReviewed == false).ToList();
            ViewData["Gifts"] = gDB.GetGifts();
            return View(l);
        }

        public IActionResult Review(int id)
        {
            Letter l = lDB.GetLetter(id);
            ViewBag.GiftName = gDB.GetGift(l.GiftId).GiftName;
            return View(l);
        }

        [HttpPost]
        public IActionResult Review(Letter l) 
        {
            if (!l.IsGood) 
            {
                //update to lump of coal if not good
                l.GiftId = 1;
            }
            l.IsReviewed = true;
            lDB.UpdateLetter(l);
            //creating a task out of this letter

            return RedirectToAction("CreateTask", "Task", l);
        }
    }
}
