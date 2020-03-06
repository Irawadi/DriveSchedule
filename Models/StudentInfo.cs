using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentK1.Models
{
    [Serializable]
    public class StudentInfo
    {
        public Bloque[] Bloques { get; set; }
        public StudentInfo() { }
    }
    [Serializable]
    public class Bloque
    {
        public string Parag { get; set; }
        public string[] Lineas { get; set; }
        public Bloque() { }
    }
}