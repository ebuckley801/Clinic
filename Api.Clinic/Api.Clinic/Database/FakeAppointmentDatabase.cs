using Library.Clinic.Models;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Clinic.Database
{
    static public class FakeAppointmentDatabase
    {
        public static int LastKey
        {
            get
            {
                if (Appointments.Any())
                {
                    return Appointments.Select(x => x.Id).Max();
                }
                return 0;
            }
        }

        private static List<Appointment> appointments = new List<Appointment>
        {
            new Appointment
            {
                Id = 1,
                StartTime = new DateTime(2024, 10, 10, 9, 0, 0),
                EndTime = new DateTime(2024, 10, 10, 10, 0, 0),
                PatientId = ObjectId.Parse("5a934e000102030405060708"),
                PhysicianId = 1
            },
            new Appointment
            {
                Id = 2,
                StartTime = new DateTime(2024, 10, 11, 11, 0, 0),
                EndTime = new DateTime(2024, 10, 11, 12, 0, 0),
                PatientId = ObjectId.Parse("5a934e000102030405060708"),
                PhysicianId = 2
            }
        };

        public static List<Appointment> Appointments
        { 
            get
            {
                return appointments;
            } 
        }

        public static Appointment? AddOrUpdateAppointment(Appointment? appointment)
        {
            if (appointment == null)
            {
                return null;
            }

            bool isAdd = false;
            if (appointment.Id <= 0)
            {
                appointment.Id = LastKey + 1;
                isAdd = true;
            }

            if (isAdd)
            {
                Appointments.Add(appointment);
            }
            else
            {
                var existingAppointment = Appointments.FirstOrDefault(a => a.Id == appointment.Id);
                if (existingAppointment != null)
                {
                    existingAppointment.StartTime = appointment.StartTime;
                    existingAppointment.EndTime = appointment.EndTime;
                    existingAppointment.PatientId = appointment.PatientId;
                    existingAppointment.PhysicianId = appointment.PhysicianId;
                }
            }

            return appointment;
        }
    }
}
