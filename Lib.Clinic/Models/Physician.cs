using Library.Clinic.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Models
{
    public  class Physician
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }

        public string license { get; set; }

        public DateTime graduationDate { get; set; }

        public List<string> specialties { get; set; }

        public List<Appointment> appointments { get; set; }

        public Physician(string name, string lastName, string license, DateTime graduationDate, List<string> specialties, List<Appointment> appointments)
        {
            this.name = name;
            this.lastName = lastName;
            this.license = license;
            this.graduationDate = graduationDate;
            this.specialties = specialties;
            this.appointments = appointments;
            Id = 0;
        }

        public Physician()
        {
            Id = 0;
            this.name = string.Empty;
            this.lastName = string.Empty;
            this.license = string.Empty;
            this.graduationDate = DateTime.MinValue;
            this.specialties = new List<string>();
            appointments = new List<Appointment>();
        }

       public void editPhysician()
        {

            bool loop = true;
            while (loop)
            {
                Console.WriteLine("1. Edit First Name");
                Console.WriteLine("2. Edit Last Name");
                Console.WriteLine("3. Edit License");
                Console.WriteLine("4. Edit Graduation Date");
                Console.WriteLine("5. Add Specialty");
                Console.WriteLine("6. Exit");
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
                        Console.WriteLine("Enter new license number: ");
                        license = Console.ReadLine() ?? string.Empty;
                        break;
                    case 4:
                        Console.WriteLine("Enter new Graduation Date: ");
                        graduationDate = Convert.ToDateTime(Console.ReadLine() ?? string.Empty);
                        break;
                    case 5:
                        Console.WriteLine("Enter New Specialty");
                        specialties.Add(Console.ReadLine() ?? string.Empty);
                        break;
                    case 6:
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            
        }

        public void addAppointment(Appointment appointment, Patient patient)
        {
            //check if the appointment is the same time as another appointment
            if (appointments.Any(x => x.Date == appointment.Date))
            {
                Console.WriteLine("There is already an appointment at that time");
            }
            //check date to see if it on a weekend or outside of the hours of 8am-5pm
            else if (appointment.Date.DayOfWeek == DayOfWeek.Saturday || appointment.Date.DayOfWeek == DayOfWeek.Sunday || appointment.Date.Hour < 8 || appointment.Date.Hour > 17)
            {
                Console.WriteLine("Appointments can only be scheduled between 8am and 5pm Monday-Friday");
            }
            else
            {
                appointments.Add(appointment);
                patient.appointments.Add(appointment);

            }
        }

        public void printSpecialties()
        {
            foreach (var specialty in specialties)
            {
                Console.WriteLine(specialty.ToString());
            }
        }

        public void printAppointments()
        {
            foreach (var appointment in appointments)
            {
                Console.WriteLine(appointment.Date.ToString());
            }
        }

        //to string method
        public override string ToString()
        {
            return $"ID: {Id}\nName: {name} {lastName}\nLicense: {license}\nGraduation Date: {graduationDate}\nSpecialties: {this.printSpecialties}\n \nAppointments: {this.printAppointments}";
        }
    }
}
