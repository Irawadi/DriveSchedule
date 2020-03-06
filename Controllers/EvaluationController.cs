using AppointmentK1.Models;
using AppointmentK1.Models.Custom;
using SVM.Communication;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace AppointmentK1.Controllers
{
    public class EvaluationController : Controller
    {
        public Student student;
        // GET: Evaluation
        [SDAuthorize]
        public ActionResult Evaluate()
        {
            ViewData["imgsrc"] = Logo();
            ViewData["Student"] = GetStudent();
            DrivingToEvaluate dte = GetDrivingToEvaluate();
            if (dte != null)
            {
                ViewData["DrivingToEvaluate"] = dte;
                return View();

            }
            else { return View("NothingToRate"); }
        }
        public ActionResult Message(string message)
        {
            ViewBag.Message = message;
            return PartialView("_Message");
        }
        public string Logo()
        {
            DSConfigurator dsc = new DSConfigurator();
            return dsc.Logo;
        }
        public ActionResult SendRating(int mark, string comment)
        {
            Student st = GetStudent();
            DrivingToEvaluate dte = (DrivingToEvaluate)Session["DrivingToEvaluate"];
            ServiceSpeaker srsp = new ServiceSpeaker(ServiceSpeaker.ConstructionMode.fromFile, @"D:\SvmWcf.xml");
            Dictionary<string, string> spParams = new Dictionary<string, string>()
            {
                ["IdDriving"] = dte.Id.ToString(),
                ["IdCategory"] = "1",
                ["IdStudent"] = st.Id.ToString(),
                ["Mark"] = mark.ToString(),
                ["Comment"] = comment
            };
            string Result = srsp.Fetch("spSDWriteDrivingRating", spParams);
            return Message("Спасибо за оценку!");
        }
        public Student GetStudent()
        {
            return student = (Student)Session["Student"];
        }
        public DrivingToEvaluate GetDrivingToEvaluate()
        {
            student = (Student)Session["Student"];
            ServiceSpeaker srsp = new ServiceSpeaker(ServiceSpeaker.ConstructionMode.fromFile, @"D:\SvmWcf.xml");
            Dictionary<string, string> spParams = new Dictionary<string, string>() { ["Param"] = student.Id.ToString() };
            string dteinfo = srsp.Fetch("spSDGetDrivingToEvaluateByIdStudent", spParams);
            DrivingToEvaluate dte;
            if (dteinfo.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(dteinfo)))
                {
                    dte = (DrivingToEvaluate)(new XmlSerializer(typeof(DrivingToEvaluate)).Deserialize(ms));
                }
                WriteDTE(dte);
            }
            else { dte = null; }
            return dte;
        }
        private void WriteDTE(DrivingToEvaluate dte)
        {
            Session["DrivingToEvaluate"] = dte;
        }
    }
}