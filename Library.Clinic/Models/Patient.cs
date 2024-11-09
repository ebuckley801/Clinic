using Library.Clinic.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public class Patient
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
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? SSN { get; set; }
        public DiagnosisEnum[]? Diagnoses { get; set; }
        public PrescriptionEnum[]? Prescriptions { get; set; }



        public Patient()
        {
            Name = string.Empty;
            Address = string.Empty;
            Birthday = DateTime.MinValue;
            Gender = string.Empty;
            SSN = string.Empty;
            Diagnoses = Array.Empty<DiagnosisEnum>();
            Prescriptions = Array.Empty<PrescriptionEnum>();
        }

        public Patient(PatientDTO p)
        {
            Id = p.Id;
            Name = p.Name;
            Birthday = p.Birthday;
            Address = p.Address;
            Gender = p.Gender;
            SSN = p.SSN;
            Diagnoses = p.Diagnoses;
            Prescriptions = p.Prescriptions;
        }
    }
}