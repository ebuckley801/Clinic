using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Clinic.Models
{
    public class Physician
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string License { get; set; }
        public DateTime GraduationDate { get; set; }
        public List<string> Specialties { get; set; }
        public List<Appointment> Appointments { get; set; }

        public Physician()
        {
            Id = 0;
            Name = string.Empty;
            LastName = string.Empty;
            License = string.Empty;
            GraduationDate = DateTime.MinValue;
            Specialties = new List<string>();
            Appointments = new List<Appointment>();
        }

        public Physician(string name, string lastName, string license, DateTime graduationDate, List<string> specialties, List<Appointment> appointments)
        {
            Name = name;
            LastName = lastName;
            License = license;
            GraduationDate = graduationDate;
            Specialties = specialties;
            Appointments = appointments;
            Id = 0;
        }
        public override string ToString()
        {
            return $"ID: {Id}\nName: {Name} {LastName}\nLicense: {License}\nGraduation Date: {GraduationDate}\nSpecialties: {string.Join(", ", Specialties)}\nAppointments: {string.Join(", ", Appointments.Select(a => a.StartTime))}";
        }
    }
}
