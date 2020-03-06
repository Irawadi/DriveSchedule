using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace AppointmentK1.Models
{
    public class DrivingToEvaluate
    {
        #region Properties
        public int Id { get; set; }
        public string Shiftname { get; set; }
        public string DateTimeBegin { get; set; }
        public string DateTimeEnd { get; set; }
        public string Master { get; set; }
        public string Vehicle { get; set; }
        public DateTime Begin
        {
            get
            {
                return ConvertStringToDateTime(DateTimeBegin);
            }
        }
        public DateTime End
        {
            get
            {
                return ConvertStringToDateTime(DateTimeEnd);
            }
        }
        public int Mark { get; set; }
        public string Comment { get; set; }
        public string Summary
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("Вождение ");
                sb.Append(DateTimeBegin);
                sb.Append(". Автомобиль ");
                sb.Append(Vehicle);
                sb.Append(". Инструктор ");
                sb.Append(Master);
                sb.Append(".");
                string result = sb.ToString();
                return result;
            }
        }
        #endregion
        #region Methods

        private DateTime ConvertStringToDateTime(string dt)
        {
            CultureInfo ci = new CultureInfo("ru-RU");
            return DateTime.ParseExact(dt, "dd.MM.yyyy HH:mm:ss", ci);
        }
        #endregion
    }
}