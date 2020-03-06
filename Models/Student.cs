using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace AppointmentK1.Models
{
    public class Student
    {
        #region Properties
        [Required(ErrorMessage ="Укажите логин")]
        [RegularExpression(@"\d*", ErrorMessage = "Логин должен состоять из цифр.")]
        [Display(Name = "Логин")]
        public string Login { get; set; }
        [Required(ErrorMessage ="Укажите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        private int IdStudent;
        public int Id { get { return IdStudent; } }
        private string surname;
        public string SurName
        {
            get { return surname; }
        }
        private string firstname;
        public string FirstName
        {
            get { return firstname; }
        }
        private string middlename;
        public string MiddleName
        {
            get { return middlename; }
        }
        private string fullname;
        public string FullName
        {
            get
            {
                fullname = firstname;
                if (middlename.Length > 0) fullname = fullname + " " + middlename;
                if (surname.Length > 0) fullname = fullname + " " + surname;
                return fullname;
            }
        }
        private string group;
        public string Group
        {
            get { return group; }
        }
        private string status;
        public string Status
        {
            get { return status; }
        }
        private bool isAuthorized;

        public bool IsAuthorized
        {
            get { return isAuthorized; }
        }

        private double usedunits;
        public double UsedUnits
        {
            get { return usedunits; }
        }
        private double acquiredunits;
        public double AcquiredUnits
        {
            get { return acquiredunits; }
        }
        public double LeftUnits
        {
            get { return acquiredunits - usedunits; }
        }
        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        #endregion
        #region Constructor
        public Student()
        {
            status = "UnAuthorized";
            isAuthorized = false;
        }
        public Student(int idStudent, string PWD)
        {
            IdStudent = idStudent;
            DSConfigurator config = new DSConfigurator();
            string info = config.StudentInfo(idStudent, PWD);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(info);
            XmlNode xnd = xDoc.SelectSingleNode("Student");
            if (xnd != null)
            {
                foreach (XmlNode xn in xnd.ChildNodes)
                {
                    switch (xn.Name)
                    {
                        case "SurName": surname = xn.InnerText; break;
                        case "FirstName": firstname = xn.InnerText; break;
                        case "MiddleName": middlename = xn.InnerText; break;
                        case "Status": status = xn.InnerText; break;
                        case "Group": group = xn.InnerText; break;
                        case "Numberofunitsacquired": acquiredunits = double.Parse(xn.InnerText); break;
                        case "Numberofunitsused": usedunits = double.Parse(xn.InnerText); break;
                    }
                }
            }
            isAuthorized = !(status == "UnAuthorized");
        }
        #endregion
        #region Methods
        public override string ToString()
        {
            string info = "";
            info += "SurName: " + surname;
            info += ". FirstName: " + firstname;
            info += ". MiddleName: " + middlename;
            info += ". Status: " + status;
            info += ". Group: " + group;
            info += ". AcquiredUnits: " + acquiredunits.ToString();
            info += ". UsedUnits: " + usedunits.ToString() + ".";
            return info;
        }
        public void LogIn()
        {
            DSConfigurator config = new DSConfigurator();
            string info = config.StudentInfo(Login, Crypt.Encrypt(Password, "df89ygy"));
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(info);
            XmlNode xnd = xDoc.SelectSingleNode("Student");
            if (xnd != null)
            {
                foreach (XmlNode xn in xnd.ChildNodes)
                {
                    switch (xn.Name)
                    {
                        case "IdStudent": IdStudent = Int32.Parse(xn.InnerText); break;
                        case "SurName": surname = xn.InnerText; break;
                        case "FirstName": firstname = xn.InnerText; break;
                        case "MiddleName": middlename = xn.InnerText; break;
                        case "Status": status = xn.InnerText; break;
                        case "Group": group = xn.InnerText; break;
                        case "Numberofunitsacquired": acquiredunits = double.Parse(xn.InnerText); break;
                        case "Numberofunitsused": usedunits = double.Parse(xn.InnerText); break;
                        case "Message": message = xn.InnerText; break;
                    }
                }
            }
            isAuthorized = !(status == "UnAuthorized");
        }
        public void LogOut()
        {
            surname = "";
            firstname = "";
            middlename = "";
            status = "UnAuthorized";
            group = "";
            acquiredunits = 0;
            usedunits = 0;
            isAuthorized = false;
            message = "";
        }
        public MySchedule ShowMySchedule()
        {
            return new MySchedule(IdStudent);
        }
        public MyExams ShowMyExams()
        {
            DSConfigurator config = new DSConfigurator();
            string info = config.FData("spGetStudentExamsK1", "IdStudent", IdStudent.ToString());
            XmlSerializer formatter = new XmlSerializer(typeof(MyExams));
            MyExams me;
            using (TextReader textReader = new StringReader(info))
            {
                me = (MyExams)formatter.Deserialize(textReader);
            }
            return me;
        }
        public StudentInfo ShowStudentInfo()
        {
            StudentInfo studentInfo = new StudentInfo();
            XmlSerializer serializer = new XmlSerializer(typeof(StudentInfo));
            DSConfigurator config = new DSConfigurator();
            string serializedXML = config.FData("spGetStudentInfo", "@IdStudent", this.Id.ToString());
            XmlSerializer formatter = new XmlSerializer(typeof(StudentInfo));
            using (TextReader textReader = new StringReader(serializedXML))
            {
                studentInfo = (StudentInfo)formatter.Deserialize(textReader);
            }
            return studentInfo;
        }
        public void Update()
        {
            DSConfigurator config = new DSConfigurator();
            string info = config.UpdateStudent(IdStudent);
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(info);
            XmlNode xnd = xDoc.SelectSingleNode("Student");
            if (xnd != null)
            {
                foreach (XmlNode xn in xnd.ChildNodes)
                {
                    switch (xn.Name)
                    {
                        case "IdStudent": IdStudent = Int32.Parse(xn.InnerText); break;
                        case "SurName": surname = xn.InnerText; break;
                        case "FirstName": firstname = xn.InnerText; break;
                        case "MiddleName": middlename = xn.InnerText; break;
                        case "Status": status = xn.InnerText; break;
                        case "Group": group = xn.InnerText; break;
                        case "Numberofunitsacquired": acquiredunits = double.Parse(xn.InnerText); break;
                        case "Numberofunitsused": usedunits = double.Parse(xn.InnerText); break;
                        case "Message": message = xn.InnerText; break;
                    }
                }
            }
            isAuthorized = !(status == "UnAuthorized");
        }
        #endregion
    }
}