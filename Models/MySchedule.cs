using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.ComponentModel.DataAnnotations;

namespace AppointmentK1.Models
{
    public class MySchedule
    {
        #region Properties
        public List<Driving> Drivings { get; set; }
        #endregion
        #region Constructor
        public MySchedule(int IdStudent)
        {
            DSConfigurator config = new DSConfigurator();
            string info = config.MySchedule(IdStudent);
            XmlDocument X = new XmlDocument();
            X.LoadXml(info);
            Drivings = new List<Driving>();
            XmlNode xnd = X.SelectSingleNode("МоёРасписание");
            if (xnd != null)
            {
                foreach (XmlNode xn in xnd.ChildNodes)
                {
                    Driving D = new Driving(xn);
                    Drivings.Add(D);
                }
            }
        }
        #endregion
    }
    public class Driving
    {
        #region Properties
        [Display(Name ="№")]
        public string Number { get; set; }
        [Display(Name = "Дата")]
        public string Date { get; set; }
        [Display(Name = "Время")]
        public string Time { get; set; }
        [Display(Name = "Авто")]
        public string Vehicle { get; set; }
        [Display(Name = "Мастер")]
        public string Instructor { get; set; }
        [Display(Name ="Место")]
        public string Place { get; set; }
        #endregion
        #region Constructor
        public Driving(XmlNode X)
        {
            if (X != null)
            {
                foreach (XmlNode xn in X.ChildNodes)
                {
                    switch (xn.Name)
                    {
                        case "Номер": Number = xn.InnerText; break;
                        case "Дата": Date = xn.InnerText; break;
                        case "Время": Time = xn.InnerText; break;
                        case "Авто": Vehicle = xn.InnerText; break;
                        case "Инструктор": Instructor = xn.InnerText; break;
                        case "Место": Place = xn.InnerText; break;
                    }
                }
            }
        }
        #endregion
    }
}