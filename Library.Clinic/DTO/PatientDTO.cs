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
    public class PatientDTO
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

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string? Name { get; set;}
        public DateTime? Birthday { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? SSN { get; set; }
        public DiagnosisEnum[]? Diagnoses { get; set; }
        public PrescriptionEnum[]? Prescriptions { get; set; }

        public PatientDTO() {
            Id = "0";
            Name = "";
            Birthday = DateTime.Now;
            Address = "";
            Gender = "";
            SSN = "";
            Diagnoses = new DiagnosisEnum[] {};
            Prescriptions = new PrescriptionEnum[] {};
        }

        public PatientDTO(Patient patient)
        {
            Id = patient.Id.ToString();
            Name = patient.Name;
            Birthday = patient.Birthday;
            Address = patient.Address;
            Gender = patient.Gender;
            SSN = patient.SSN;
            Diagnoses = patient.Diagnoses;
            Prescriptions = patient.Prescriptions;
        }
    }
}