using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppointmentK1.Models
{
/// <summary>
/// Класс для работы с моделями
/// </summary>
    public class FatModel
    {
        public Student Student { get; set; }
        public DSSchedule Schedule { get; set; }
    }
}