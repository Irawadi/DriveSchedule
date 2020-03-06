using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace AppointmentK1.Models
{
    /// <summary>
    /// Таблица Расписания для определённого места вождения
    /// </summary>
    public class Place
    {
        #region Properties
        public int id { get; private set; }
        public string Name { get; private set; }
        private List<Shift> _shifts;

        public List<Shift> Shifts
        {
            get { return _shifts; }
            set { _shifts = value; }
        }
        #endregion
        #region Constructor
        internal Place(XmlNode xnd)
        {
            XmlDocument xRoot = new XmlDocument();
            xRoot.LoadXml(xnd.OuterXml);
            _shifts = new List<Shift>();
            id = Convert.ToInt32(xRoot.SelectSingleNode("//Place/Id").InnerText);
            Name = xRoot.SelectSingleNode("//Place/Name").InnerText;
            XmlNodeList shft = xRoot.SelectNodes("//Place/Shifts/Shift");
            foreach (XmlNode xn in shft)
                _shifts.Add(new Shift(xn));
        }
        public Place()
        {
            id = 0;
            Name = "Место вождения не определено";
            _shifts = new List<Shift>();
        }
        #endregion
    }
}