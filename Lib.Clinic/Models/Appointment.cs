using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int PhysicianId { get; set; }

        public DateTime Date { get; set; }
        public Appointment()
        {
            Id = 0;
            PatientId = 0;
            PhysicianId = 0;
            Date = DateTime.MinValue;
        }
    }
}
