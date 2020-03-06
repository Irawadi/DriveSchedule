using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace AppointmentK1.Models
{
    /// <summary>
    ////Смена в определённый день
    /// </summary>
    public class Shift
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
        private List<Vehicle> _vehicles;

        public List<Vehicle> Vehicles
        {
            get { return _vehicles; } set { _vehicles = value; }
        }
        #endregion
        #region Constructor
        public Shift(XmlNode xn)
        {
            XmlDocument xRoot = new XmlDocument();
            xRoot.LoadXml(xn.OuterXml);
            _vehicles = new List<Vehicle>();
            id = Convert.ToInt32(xRoot.SelectSingleNode("//Shift/Id").InnerText);
            name = xRoot.SelectSingleNode("//Shift/Shift").InnerText;
            XmlNodeList vcls = xRoot.SelectNodes("//Shift/Vehicles/Vehicle");
            XmlDocument X = new XmlDocument();
            foreach (XmlNode xnd in vcls)
            {
                X.LoadXml(xnd.OuterXml);
                _vehicles.Add(new Vehicle(X.DocumentElement));
            }
        }
        #endregion
    }
}