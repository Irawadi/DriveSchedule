using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointmentK1.Models;
using AppointmentK1.Models.Custom;

namespace AppointmentK1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            ViewData["imgsrc"] =Logo();
            return View();
        }
        [HttpPost]
        public ActionResult Index(Student student)
        {
            if (ModelState.IsValid)
            {
                student.LogIn();
                if (student.IsAuthorized)
                {
                    Session["Student"] = student;
                    DSSchedule Schedule = new DSSchedule(DateTime.Now, student.Id);
                    Session["Schedule"] = Schedule;
                    return RedirectToAction("Show", "Schedule");
                }
                else
                {
                    if (student.Message == "")
                    {
                        ModelState.AddModelError("", "Логин или пароль некорректны");
                    }
                    else
                    {
                        ModelState.AddModelError("", student.Message);
                    }
                    Session["Student"] = null;
                    ViewData["imgsrc"] = Logo();
                    return View(student);
                }
            }
            else
            {
                ViewData["imgsrc"] = Logo();
                return View();
            }
        }
        public ActionResult Logout()
        {
            try
            {
                Session["Student"] = null;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public string Logo()
        {
            DSConfigurator dsc = new DSConfigurator();
            return dsc.Logo;
        }
    }
}