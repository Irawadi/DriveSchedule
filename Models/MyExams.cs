using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentK1.Models
{
    [Serializable]
    public class MyExams
    {
        public List<Exam> Exams { get; set; }
        public MyExams() { }
    }
    [Serializable]
    public class Exam
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Result { get; set; }
        public Exam() { }
    }
}