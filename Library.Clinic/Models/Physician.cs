using System;
using System.Collections.Generic;
using System.Linq;
using Library.Clinic.DTO;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public string? Name { get; set; }
        public string? License { get; set; }
        public DateTime? GraduationDate { get; set; }
        public string? Specialty { get; set; }
        public List<Appointment>? Appointments { get; set; }

        public Physician()
        {
            Name = string.Empty;
            License = string.Empty;
            GraduationDate = DateTime.MinValue;
            Specialty = string.Empty;
            Appointments = new List<Appointment>();
        }

        public Physician(PhysicianDTO p)
        {
            if(p.Id != "0")
            {
                Id = ObjectId.Parse(p.Id);
            }
            else
            {
                Id = ObjectId.GenerateNewId();
            }
            Name = p.Name;
            License = p.License;
            GraduationDate = p.GraduationDate;
            Specialty = p.Specialty;
        }
    }
}
