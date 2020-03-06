using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.ComponentModel.DataAnnotations;

namespace AppointmentK1.Models
{
    /// <summary>
    /// Расписание по дню и слушателю
    /// </summary>
    public class DSSchedule
    {
        #region Properties
        private DateTime day;
        [Required(ErrorMessage = "Укажите дату")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        [Display(Name = "Дата")]
        public DateTime Day
        {
            get { return day; }
            set { day = value; DSHelper.Day = value; }
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; DSHelper.Message = value; }
        }
        public string DayString
        { get { return DSHelper.RussianDate(Day); } }
        private List<Place> _places { get; set; }
        private XmlDocument _body { get; set; }
        public List<Place> Places
        {
            get { return _places; }
        }
        private int idstudent = 0;
        /// <summary>
        /// id выбранного места
        /// </summary>
        private int selectedplace = 0;
        /// <summary>
        /// Выбранное место вождения
        /// </summary>
        public Place SelectedPlace
        {
            get {
                foreach (Place p in _places)
                {
                    if (p.id == selectedplace) { return p; }
                }
                return new Place();
            }
        }
        private string buttonmessage;
        /// <summary>
        /// Результат последнего нажатия кнопки
        /// </summary>
        public string ButtonMessage
        {
            get { return buttonmessage; }
        }

        #endregion
        #region Constructor
        public DSSchedule()
        {
            Day = DateTime.Today;
            _places = new List<Place>();
            Message = "Test Message";
        }
        public DSSchedule(DateTime day, int IdStudent)
        {
            Day = day;
            idstudent = IdStudent;
            Construct();
        }
        public DSSchedule(DateTime day, Student student)
        {
            Day = day;
            idstudent = student.Id;
            Construct();
        }
        private void Construct()
        {
            Message = "Just Created Schedule for " + Day.ToShortDateString();
            _places = new List<Place>();
            BuildFromXml();
        }
        private void BuildFromXml()
        {
            buttonmessage = "";
            GetTheXml();
            XmlElement xRoot = _body.DocumentElement;
            //Message
            XmlNode xn = xRoot.SelectSingleNode("//Day/Message");
            Message = xn.InnerText;
            //Places
            Place p;
            _places.Clear();
            XmlNodeList childnodes = xRoot.SelectNodes("//Day/Place");
            XmlDocument X = new XmlDocument();
            foreach (XmlNode n in childnodes)
            {
                X.LoadXml(n.OuterXml);
                p = new Place(X.DocumentElement);
                _places.Add(p);
                if (selectedplace == 0) { selectedplace = p.id; }
            }
        }
        private void GetTheXml()
        {
            DSConfigurator config = new DSConfigurator();
            string info = config.Schedule(idstudent, day);
            _body = new XmlDocument();
            _body.LoadXml(info);
        }
        #endregion
        #region Methods
        /// <summary>
        /// Заполняет расписание для конкретного слушателя при установленной дате
        /// </summary>
        /// <param name="idstudent"></param>
        public void Load(int IdStudent)
        {
            idstudent = IdStudent;
            BuildFromXml();
        }
        public void Load()
        {
            BuildFromXml();
        }
        /// <summary>
        /// Показывает расписание следующего дня
        /// </summary>
        public void NextDay()
        {
            Day = Day.AddDays(1);
            BuildFromXml();
        }
        /// <summary>
        /// Показывает расписание предыдущего дня
        /// </summary>
        public void PreviousDay()
        {
            Day = Day.AddDays(-1);
            BuildFromXml();
        }
        /// <summary>
        /// Выбор места вождения по id
        /// </summary>
        /// <param name="placeid"></param>
        public void SelectPlace(int placeid)
        {
            selectedplace = placeid;
        }        
        /// <summary>
        /// Смена даты
        /// </summary>
        /// <param name="day"></param>
        public void SelectDay(DateTime day)
        {
            Day = day;
            BuildFromXml();
        }
        public bool IsSelected(Place p)
        {
            return (p.id == selectedplace);
        }
        /// <summary>
        /// Нажать кнопку (Записаться или отменить)
        /// </summary>
        /// <param name="buttonValue">Значение кнопки в формате "IdShift|IdShift|IdVehicle|IdVehicle|"</param>
        public void PressButton(string buttonValue)
        {
            DSHelper.ClearValue();
            DSHelper.Delimiter = "|";
            DSHelper.BuildStringWithDelimeter("IdStudent", idstudent.ToString());
            DSHelper.BuildStringWithDelimeter("IdPlace", selectedplace.ToString());
            DSHelper.BuildStringWithDelimeter("WayOfAssignment", "DrivingSite");
            DSHelper.BuildStringWithDelimeter("DateDrive", DSHelper.RussianDate(Day));
            string info = DSHelper.Value+buttonValue;
            DSConfigurator config = new DSConfigurator();
            info = config.Subscribe(info);
            BuildFromXml();
            buttonmessage = info;
        }
        #endregion
    }
}