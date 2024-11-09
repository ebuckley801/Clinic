using Api.Clinic.Database;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api.Clinic.Enterprise
{
    public class AppointmentEC
    {
        public IEnumerable<AppointmentDTO> Appointments
        {
            get
            {
                return FakeAppointmentDatabase.Appointments.Take(100).Select(a => new AppointmentDTO(a));
            }
        }

        public IEnumerable<AppointmentDTO>? Search(string query)
        {
            return FakeAppointmentDatabase.Appointments
                .Where(a => a.PatientId.ToString().Contains(query) ||
                            a.PhysicianId.ToString().Contains(query) ||
                            a.StartTime.ToString().Contains(query) ||
                            a.EndTime.ToString().Contains(query))
                .Select(a => new AppointmentDTO(a));
        }

        public AppointmentDTO? GetById(int id)
        {
            var appointment = FakeAppointmentDatabase.Appointments
                .FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                return new AppointmentDTO(appointment);
            }

            return null;
        }

        public AppointmentDTO? Delete(int id)
        {
            var appointmentToDelete = FakeAppointmentDatabase.Appointments.FirstOrDefault(a => a.Id == id);
            if (appointmentToDelete != null)
            {
                FakeAppointmentDatabase.Appointments.Remove(appointmentToDelete);
                return new AppointmentDTO(appointmentToDelete);
            }

            return null;
        }

        public Appointment? AddOrUpdate(AppointmentDTO? appointmentDto)
        {
            if (appointmentDto == null)
            {
                return null;
            }

            return FakeAppointmentDatabase.AddOrUpdateAppointment(new Appointment(appointmentDto));
        }
    }
}
