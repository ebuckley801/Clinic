using Library.Clinic.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Library.Clinic.DTO
{
    public class AppointmentDTO
    {
        public override string ToString()
        {
            return $"[{Id}] {StartTime} - {EndTime}";
        }

        public string Display
        {
            get => $"[{Id}] {StartTime} - {EndTime}";
        }


        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId PatientId { get; set; }
        public int PhysicianId { get; set; }
        public PhysicianDTO? Physician { get; set; }
        public PatientDTO? Patient { get; set; }

        public AppointmentDTO() { }
        public AppointmentDTO(Appointment a)
        {
            Id = a.Id;
            StartTime = a.StartTime;
            EndTime = a.EndTime;
            PatientId = a.PatientId;
            PhysicianId = a.PhysicianId;
            Physician = a.Physician;
            Patient = a.Patient;
        }
    }
}