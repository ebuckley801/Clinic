using Library.Clinic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.Clinic.Models
{
    public class Appointment
    {
        public Appointment() { }

        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId PatientId { get; set; }

        public int PhysicianId { get; set; }
        public PhysicianDTO? Physician { get; set; }
        public PatientDTO? Patient { get; set; }

        public Appointment(AppointmentDTO a)
        {
            Id = a.Id;
            StartTime = a.StartTime;
            EndTime = a.EndTime;
            PatientId = ObjectId.Parse(a.PatientId);
            PhysicianId = a.PhysicianId;
            Physician = a.Physician;
            Patient = a.Patient;
        }
    }
}