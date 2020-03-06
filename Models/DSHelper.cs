using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentK1.Models
{
    public static class DSHelper
    {
        #region Properties
        private static string _value;

        public static string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private static string delimeter;

        public static string Delimiter
        {
            get { return delimeter; }
            set { delimeter = value; }
        }

        public static DateTime Day { get; set; }
        public static string Message { get; set; }
        #endregion
        #region Methods
        public static string RussianDate(DateTime Day)
        {
            return $"{Right("0"+Day.Day.ToString(), 2)}.{Right("0" + Day.Month.ToString(), 2)}.{Day.Year}";
        }
        private static string Right(this string sValue, int iMaxLength)
        {
            //Check if the value is valid
            if (string.IsNullOrEmpty(sValue))
            {
                //Set valid empty string as string could be null
                sValue = string.Empty;
            }
            else if (sValue.Length > iMaxLength)
            {
                //Make the string no longer than the max length
                sValue = sValue.Substring(sValue.Length - iMaxLength, iMaxLength);
            }

            //Return the string
            return sValue;
        }
        public static void Clear() { _value = ""; delimeter = ""; }
        public static void ClearValue() { _value = ""; }
        public static void BuildStringWithDelimeter(string key, string value)
        {
            _value = _value + key + delimeter + value + delimeter;
        }
        #endregion
    }
}