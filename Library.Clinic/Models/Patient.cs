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
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
        public string? SSN { get; set; }


        public Patient()
        {
            Name = string.Empty;
            Address = string.Empty;
            Birthday = DateTime.MinValue;
            SSN = string.Empty;
        }

        public Patient(PatientDTO p)
        {
            Id = p.Id;
            Name = p.Name;
            Birthday = p.Birthday;
            Address = p.Address;
            SSN = p.SSN;
        }
    }
}