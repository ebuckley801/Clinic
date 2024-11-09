using Library.Clinic.Models;

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
        public int PatientId { get; set; }
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