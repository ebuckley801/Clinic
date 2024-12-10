using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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

        [BsonRepresentation(BsonType.ObjectId)] 
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? License { get; set; }
        public DateTime? GraduationDate { get; set; }
        public string? Specialty { get; set; }
        public List<Appointment>? Appointments { get; set; }

        public PhysicianDTO() {
            Id = "0";
            Name = "";
            License = "";
            GraduationDate = DateTime.Now;
            Specialty = "";
            Appointments = new List<Appointment>();
         }
        public PhysicianDTO(Physician p)
        {
            Id = p.Id.ToString();
            Name = p.Name;
            License = p.License;
            GraduationDate = p.GraduationDate;
            Specialty = p.Specialty;
            Appointments = p.Appointments;
        }
    }
}