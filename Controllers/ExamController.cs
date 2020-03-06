using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointmentK1.Models.Custom;
using AppointmentK1.Models;

namespace AppointmentK1.Controllers
{
    public class ExamController : Controller
    {
        public Student student;
        public ActionResult Exam()
        {
            student = GetStudent();
            ViewData["Student"] = student;
            ViewData["imgsrc"] = Logo();
            return View();
        }
        public Student GetStudent()
        {
            return (Student)Session["Student"];
        }
        public string Logo()
        {
            DSConfigurator dsc = new DSConfigurator();
            return dsc.Logo;
        }
    }
}