using System;
using System.Collections.Generic;
using System.Linq;
using Library.Clinic.DTO;
namespace Library.Clinic.Models
{
    public class Physician
    {
        public override string ToString()
        {
            return $"ID: {Id}\nName: {Name} \nLicense: {License}\nGraduation Date: {GraduationDate}\nSpecialty: {Specialty}\nAppointments: {string.Join(", ", Appointments.Select(a => a.StartTime))}";
        }

        public string Display
        {
            get => $"[{Id}] {Name}";
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? License { get; set; }
        public DateTime? GraduationDate { get; set; }
        public string? Specialty { get; set; }
        public List<Appointment>? Appointments { get; set; }

        public Physician()
        {
            Id = 0;
            Name = string.Empty;
            License = string.Empty;
            GraduationDate = DateTime.MinValue;
            Specialty = string.Empty;
            Appointments = new List<Appointment>();
        }

        public Physician(string name, string license, DateTime graduationDate, string specialty, List<Appointment> appointments)
        {
            Name = name;
            License = license;
            GraduationDate = graduationDate;
            Specialty = specialty;
            Appointments = appointments;
            Id = 0;
        }

        public Physician(PhysicianDTO p)
        {
            Id = p.Id;
            Name = p.Name;
            License = p.License;
            GraduationDate = p.GraduationDate;
            Specialty = p.Specialty;
        }
    }
}
