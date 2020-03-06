using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace AppointmentK1.Models
{
    /// <summary>
    /// Авто в определённую смену
    /// </summary>
    public class Vehicle
    {
        #region Properties
        private int id;

        public int Id
        {
            get { return id; } set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; } set { name = value; }
        }
        private string shortname;

        public string ShortName
        {
            get { return shortname; } set { shortname = value; }
        }
        private string master;

        public string Master
        {
            get { return master; } set { master = value; }
        }
        private string fullname;

        public string FullName
        {
            get { return fullname; } set { fullname = value; }
        }
        private string status;

        public string Status
        {
            get { return status; } set { status = value; }
        }
        private string allowedtosubscribe;

        public string AllowedToSubscribe
        {
            get { return allowedtosubscribe; } set { allowedtosubscribe = value; }
        }
        private string allowedtounsubscribe;

        public string AllowedToUnsubscribe
        {
            get { return allowedtounsubscribe; } set { allowedtounsubscribe = value; }
        }
        public bool SubscribeAllowed
        { get { return (allowedtosubscribe == "Yes"); } }
        public bool UnsubscribeAllowed
        { get { return (allowedtounsubscribe=="Yes"); } }
        #endregion
        #region Constructor
        public Vehicle(XmlNode xn)
        {
            id = Convert.ToInt32(xn.SelectSingleNode("//Vehicle/Id").InnerText);
            name = xn.SelectSingleNode("//Vehicle/Name").InnerText;
            shortname = xn.SelectSingleNode("//Vehicle/ShortName").InnerText;
            master = xn.SelectSingleNode("//Vehicle/Master").InnerText;
            fullname = xn.SelectSingleNode("//Vehicle/FullName").InnerText;
            status = xn.SelectSingleNode("//Vehicle/Status").InnerText;
            allowedtosubscribe = xn.SelectSingleNode("//Vehicle/AllowedToSubscribe").InnerText;
            allowedtounsubscribe = xn.SelectSingleNode("//Vehicle/AllowedToUnsubscribe").InnerText;
        }
        #endregion
    }
}