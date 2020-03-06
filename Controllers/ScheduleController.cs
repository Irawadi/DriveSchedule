using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AppointmentK1.Models;
using AppointmentK1.Models.Custom;
using System.Globalization;

namespace AppointmentK1.Controllers
{
    [SDAuthorize]
    public class ScheduleController : Controller
    {
        public Student student;

        [HttpGet]
        public ActionResult Show()
        {
            DSSchedule sch = new DSSchedule(DateTime.Today, GetStudent());
            Session["Schedule"] = sch;
            ViewData["FatModel"] = GetFatModel();
            ViewData["imgsrc"] = Logo();
            return View();
        }

        [HttpPost]
        public ActionResult Show(string i2)
        {
            FatModel FM = GetFatModel();
            switch (i2)
            {
                case "Next": FM.Schedule.NextDay(); break;
                case "Prev": FM.Schedule.PreviousDay(); break;
                default: break;
            }
            ViewData["FatModel"] = FM;
            ViewData["imgsrc"] = Logo();
            WriteFatModel(FM);
            return PartialView("_ShowPartial", FM);
        }

        public ActionResult Next()
        {
            FatModel FM = GetFatModel();
            FM.Schedule.NextDay();
            ViewData["FatModel"] = FM;
            ViewData["imgsrc"] = Logo();
            WriteFatModel(FM);
            return PartialView("_ShowPartial", FM);
        }
        public ActionResult Prev()
        {
            FatModel FM = GetFatModel();
            FM.Schedule.PreviousDay();
            ViewData["FatModel"] = FM;
            ViewData["imgsrc"] = Logo();
            WriteFatModel(FM);
            return PartialView("_ShowPartial", FM);
        }
        public ActionResult Aj(string SDate)
        {
            FatModel FM = GetFatModel();
            DateTime dateValue;
            if (DateTime.TryParseExact(SDate, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateValue))
            {
                FM.Schedule.SelectDay(dateValue);
                WriteFatModel(FM);
            }
            else { FM.Schedule.Message = "Строка " + SDate + " не является датой"; }
            ViewData["FatModel"] = FM;
            ViewData["imgsrc"] = Logo();
            return PartialView("_ShowPartial", FM);
        }

        public ActionResult Rc(string PlaceId)
        {
            FatModel FM = GetFatModel();
            int placeId;
            if (Int32.TryParse(PlaceId, out placeId)) { FM.Schedule.SelectPlace(placeId); }
            else { FM.Schedule.SelectPlace(0); }
            WriteFatModel(FM);
            ViewData["FatModel"] = FM;
            ViewData["imgsrc"] = Logo();
            return PartialView("_ShowPartial", FM);
        }

        public ActionResult PressButton(string buttonValue)
        {
            FatModel FM = GetFatModel();
            FM.Schedule.PressButton(buttonValue);
            FM.Student.Update();
            WriteFatModel(FM);
            ViewData["FatModel"] = FM;
            ViewData["imgsrc"] = Logo();
            return PartialView("_ShowPartial", FM);
        }
        
        public Student GetStudent()
        {
            return student = (Student)Session["Student"];
        }
        public DSSchedule GetSchedule()
        {
            return (DSSchedule)Session["Schedule"];
        }
        public FatModel GetFatModel()
        {
            FatModel FM = new FatModel()
            {
                Student = GetStudent(),
                Schedule = GetSchedule()
            };
            return FM;
        }
        public void WriteFatModel(FatModel FM)
        {
            Session["Student"] = FM.Student;
            Session["Schedule"] = FM.Schedule;
        }
        public string Logo()
        {
            DSConfigurator dsc = new DSConfigurator();
            return dsc.Logo;
        }
    }
}