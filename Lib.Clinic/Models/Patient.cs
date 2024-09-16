using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
     public class Patient
    {
        public string name { get; set; }
        public string lastName { get; set; }

        public string address { get; set; }

        public DateTime birthdate { get; set; }

        public string race { get; set; }

        public string sex { get; set; }

        public int Id { get; set; }
        public List<String> diagnoses { get; set; }

        public List<String> prescriptions { get; set; }

        public List<Appointment> appointments { get; set; }

        public Patient(string name, string lastName, string address, DateTime birthdate, string race, string sex, int Id, List<String> diagnoses, List<String> prescriptions, List<Appointment> appointments)
        {
            this.name = name;
            this.lastName = lastName;
            this.address = address;
            this.birthdate = birthdate;
            this.race = race;
            this.sex = sex;
            this.Id = Id;
            this.diagnoses = diagnoses;
            this.prescriptions = prescriptions;
            this.appointments = appointments;
        }

        public Patient()
        {
            name = string.Empty;
            lastName = string.Empty;
            address = string.Empty;
            birthdate = DateTime.MinValue;
            race = string.Empty;
            sex = string.Empty;
            Id = 0;
            prescriptions = new List<string>();
            diagnoses = new List<string>();
            appointments = new List<Appointment>();
        }



        public void editPatient()
        {
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("1. Edit First Name");
                Console.WriteLine("2. Edit Last Name");
                Console.WriteLine("3. Edit Address");
                Console.WriteLine("4. Edit Birthdate");
                Console.WriteLine("5. Edit Race");
                Console.WriteLine("6. Edit Sex");
                Console.WriteLine("7.Add Diagnosis");
                Console.WriteLine("8. Add Prescription");
                Console.WriteLine("9. Exit");
                Console.WriteLine("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine() ?? string.Empty);  // ?? string.Empty is used to avoid null exception
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter new First Name: ");
                        name = Console.ReadLine() ?? string.Empty;
                        break;
                    case 2:
                        Console.WriteLine("Enter new Last Name: ");
                        lastName = Console.ReadLine() ?? string.Empty;
                        break;
                    case 3:
                        Console.WriteLine("Enter new Address: ");
                        address = Console.ReadLine() ?? string.Empty;
                        break;
                    case 4:
                        Console.WriteLine("Enter new Birthdate: ");
                        birthdate = Convert.ToDateTime(Console.ReadLine() ?? string.Empty);
                        break;
                    case 5:
                        Console.WriteLine("Enter Race");
                        race = Console.ReadLine() ?? string.Empty;
                        break;
                    case 6:
                        Console.WriteLine("Enter Sex");
                        sex = Console.ReadLine() ?? string.Empty;
                        break;
                    case 7:
                        Console.WriteLine("Enter Diagnosis");
                        diagnoses.Add(Console.ReadLine() ?? string.Empty);
                        break;
                    case 8:
                        Console.WriteLine("Enter Prescription");
                        prescriptions.Add(Console.ReadLine() ?? string.Empty);
                        break;
                    case 9:
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }

        public void printDiagnoses()
        {
            foreach (var diagnosis in diagnoses)
            {
                Console.WriteLine(diagnosis.ToString());
            }
        }

        public void printPrescriptions()
        {
            foreach (var prescription in prescriptions)
            {
                Console.WriteLine(prescription.ToString());
            }
        }

        public void printAppointments()
        {
            foreach (var appointment in appointments)
            {
                Console.WriteLine(appointment.ToString());
            }
        }

        //ToString method to display patient information
        public override string ToString()
        {
            return $"ID: {Id}\nName: {name} {lastName}\nAddress: {address}\nBirthdate: {birthdate}\nRace: {race}\nSex: {sex}\nDiagnoses: {this.printDiagnoses}\nPrescriptions: {this.printAppointments}\nAppointments: {this.printAppointments}\n";
        }



    }
}
