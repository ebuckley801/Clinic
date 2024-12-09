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
    public class Patient
    {
        public override string ToString()
        {
            return $"{Name}";
        }

        //TODO: Remove this and put it on a ViewModel instead
        public string Display
        {
            get
            {
                return $"{Name}";
            }
        }

        [BsonId]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
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

        public Patient(PatientDTO patientDTO)
        {
            if(patientDTO.Id != "0")
            {
                Id = ObjectId.Parse(patientDTO.Id);
            }
            else
            {
                Id = ObjectId.GenerateNewId();
            }
            Name = patientDTO.Name;
            Birthday = patientDTO.Birthday;
            Address = patientDTO.Address;
            Gender = patientDTO.Gender;
            SSN = patientDTO.SSN;
            Diagnoses = patientDTO.Diagnoses;
            Prescriptions = patientDTO.Prescriptions;
        }
    }
}