using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointmentK1.Models.Custom;
using AppointmentK1.Models;

namespace AppointmentK1.Controllers
{
    public class MyScheduleController : Controller
    {
        public Student student;
        // GET: MySchedule
        [SDAuthorize]
        public ActionResult Index()
        {
            student = GetStudent();
            ViewData["Student"] = student;
            ViewData["imgsrc"] = Logo();
            List<Driving> Drivings = student.ShowMySchedule().Drivings;
            return View(Drivings);
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