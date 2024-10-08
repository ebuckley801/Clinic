//initialize console app
using System;
using System.ComponentModel.Design;
using System.Xml.Serialization;
using Library.Clinic.Models;
using Library.Clinic.Services;

namespace ConsoleApp
{
    internal class PatientManager
    {
        static void menu()
        {
            int choice;
            do
            {
                Console.WriteLine("Welcome to the Patient Manager");
                Console.WriteLine("1. Add Patient");
                Console.WriteLine("2. Delete Patient");
                Console.WriteLine("3. Edit Petient Profile");
                Console.WriteLine("4. View Patient Profile");
                Console.WriteLine("5. Add Physician");
                Console.WriteLine("6. Delete Physician");
                Console.WriteLine("7. Edit Physician Profile");
                Console.WriteLine("8. View Physician Profile");
                Console.WriteLine("9. Book Appointment");
                Console.WriteLine("10. View Appointments");
                Console.WriteLine("11. Exit");
                Console.WriteLine("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine() ?? string.Empty);  // ?? string.Empty is used to avoid null exception
                switch (choice)
                {
                    case 1://add patient
                        Console.WriteLine("Please enter the patient first name: ");
                        var name = Console.ReadLine();
                        var newPatient = new Patient { name = name ?? string.Empty };
                        PatientServiceProxy.Current.AddPatient(newPatient);
                        break;
                    case 2://delete patient
                        PatientServiceProxy.Current.Patients.ForEach(x => Console.WriteLine($"{x.Id}. {x.name}"));
                        int selectedPatient = int.Parse(Console.ReadLine() ?? "-1");
                        PatientServiceProxy.Current.DeletePatient(selectedPatient);
                        break;
                    case 3:
                        PatientServiceProxy.Current.Patients.ForEach(x => Console.WriteLine($"{x.Id}. {x.name}"));
                        Console.WriteLine("Please enter the patient ID you want to edit: ");
                        int patientId = int.Parse(Console.ReadLine() ?? "-1");
                        Patient p = PatientServiceProxy.Current.FindPatient(patientId);
                        if (p != null)
                        {
                            p.editPatient();
                        }
                        else
                        {
                            Console.WriteLine("Patient not found");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Please enter the patient ID you want to view: ");
                        patientId = int.Parse(Console.ReadLine() ?? "-1");
                        p = PatientServiceProxy.Current.FindPatient(patientId);
                        if (p != null)
                        {
                            Console.WriteLine(p.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Patient not found");
                        }
                        break;
                    case 5:
                        Console.WriteLine("Please enter the physician name: ");
                        var pName = Console.ReadLine();
                        var newPhysician = new Physician { name = pName ?? string.Empty };
                        PhysicianServiceProxy.AddPhysician(newPhysician);
                        break;
                    case 6:
                        PhysicianServiceProxy.Physicians.ForEach(x => Console.WriteLine($"{x.Id}. {x.name}"));
                        int selectedPhysician = int.Parse(Console.ReadLine() ?? "-1");
                        PhysicianServiceProxy.DeletePhysician(selectedPhysician);
                        break;
                    case 7:
                        PhysicianServiceProxy.Physicians.ForEach(x => Console.WriteLine($"{x.Id}. {x.name}"));
                        Console.WriteLine("Please enter the physician ID you want to edit: ");
                        int physicianId = int.Parse(Console.ReadLine() ?? "-1");
                        Physician physician = PhysicianServiceProxy.FindPhysician(physicianId);
                        if (physician != null)
                        {
                            physician.editPhysician();
                        }
                        else
                        {
                            Console.WriteLine("Physician not found");
                        }
                        break;
                    case 8:
                        Console.WriteLine("Please enter the physician ID you want to view: ");
                        physicianId = int.Parse(Console.ReadLine() ?? "-1");
                        physician = PhysicianServiceProxy.FindPhysician(physicianId);
                        if (physician != null)
                        {
                            Console.WriteLine(physician.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Physician not found");
                        }
                        break;
                    case 9:
                        Console.WriteLine("Please enter the patient ID: ");
                        int pId = int.Parse(Console.ReadLine() ?? "-1");
                        if(PatientServiceProxy.Current.FindPatient(pId) == null)
                        {
                            Console.WriteLine("Patient not found");
                            break;
                        }
                        Console.WriteLine("Please enter the physician ID: ");
                        int phyId = int.Parse(Console.ReadLine() ?? "-1");
                        if (PhysicianServiceProxy.FindPhysician(phyId) == null)
                        {
                            Console.WriteLine("Physician not found");
                            break;
                        }
                        Console.WriteLine("Please enter the appointment date: ");
                        DateTime date = Convert.ToDateTime(Console.ReadLine() ?? string.Empty);
                        Appointment appointment = new Appointment { PatientId = pId, PhysicianId = phyId, Date = date };
                        PhysicianServiceProxy.FindPhysician(phyId).appointments.Add(appointment);
                        break;
                    case 10:
                        Console.WriteLine("Please enter the physician ID: ");
                        int physicianID = int.Parse(Console.ReadLine() ?? "-1");
                        PhysicianServiceProxy.FindPhysician(physicianID).appointments.ForEach(x => Console.WriteLine(x.Date));
                        break;
                    case 11:
                        Console.WriteLine("Goodbye");
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            } while (choice != 11);
        }
        static void Main(string[] args)
        {
            menu();
        }
    }
}