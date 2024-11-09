using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.DTO
{
    public class PhysicianDTO
    {
        public override string ToString()
        {
            return $"[{Id}] {Name}";
        }

        //TODO: Remove this and put it on a ViewModel instead
        public string Display
        {
            get
            {
                return $"[{Id}] {Name}";
            }
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? License { get; set; }
        public DateTime? GraduationDate { get; set; }
        public string? Specialty { get; set; }
        public List<Appointment>? Appointments { get; set; }

        public PhysicianDTO() { }
        public PhysicianDTO(Physician p)
        {
            Id = p.Id;
            Name = p.Name;
            License = p.License;
            GraduationDate = p.GraduationDate;
            Specialty = p.Specialty;
            Appointments = p.Appointments;
        }
    }
}