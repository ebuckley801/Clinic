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

        public string PatientId { get; set; }
        public string PhysicianId { get; set; }
        public PhysicianDTO? Physician { get; set; }
        public PatientDTO? Patient { get; set; }

        public AppointmentDTO() { }
        public AppointmentDTO(Appointment a)
        {
            Id = a.Id;
            StartTime = a.StartTime;
            EndTime = a.EndTime;
            PatientId = a.PatientId.ToString();
            PhysicianId = a.PhysicianId.ToString();
            Physician = a.Physician;
            Patient = a.Patient;
        }
    }
}