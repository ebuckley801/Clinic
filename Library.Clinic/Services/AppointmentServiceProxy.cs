using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public class AppointmentServiceProxy
    {
        private static object _lock = new object();

        private static AppointmentServiceProxy? instance;
        public static AppointmentServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new AppointmentServiceProxy();
                    }
                }
                return instance;
            }
        }

        private AppointmentServiceProxy()
        {
            instance = null;
            appointments = new List<AppointmentDTO>();
            var appointmentsData = new WebRequestHandler().Get("/Appointment").Result;
            Appointments = JsonConvert.DeserializeObject<List<AppointmentDTO>>(appointmentsData) ?? new List<AppointmentDTO>();
        }

        private int LastKey
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

        private List<AppointmentDTO> appointments;
        public List<AppointmentDTO> Appointments
        {
            get
            {
                return appointments;
            }
            private set
            {
                if (appointments != value)
                {
                    appointments = value;
                }
            }
        }

        public async Task<List<AppointmentDTO>> Search(string query)
        {
            try
            {
                var appointmentsPayload = await new WebRequestHandler()
                    .Post("/Appointment/Search", new Query(query));

                Appointments = JsonConvert.DeserializeObject<List<AppointmentDTO>>(appointmentsPayload)
                    ?? new List<AppointmentDTO>();

                return Appointments;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them)
                Console.WriteLine($"Error searching appointments: {ex.Message}");
                return new List<AppointmentDTO>();
            }
        }

        public async Task<AppointmentDTO?> AddOrUpdateAppointment(AppointmentDTO appointment)
        {
            try
            {
                var payload = await new WebRequestHandler().Post("/Appointment", appointment);
                var newAppointment = JsonConvert.DeserializeObject<AppointmentDTO>(payload);

                if (newAppointment != null)
                {
                    if (newAppointment.Id > 0 && appointment.Id == 0)
                    {
                        // New appointment to be added to the list
                        Appointments.Add(newAppointment);
                    }
                    else if (newAppointment.Id > 0 && appointment.Id > 0 && appointment.Id == newAppointment.Id)
                    {
                        // Edit: Replace the existing appointment in the list
                        var currentAppointment = Appointments.FirstOrDefault(a => a.Id == newAppointment.Id);
                        var index = Appointments.Count;
                        if (currentAppointment != null)
                        {
                            index = Appointments.IndexOf(currentAppointment);
                            Appointments.RemoveAt(index);
                        }
                        Appointments.Insert(index, newAppointment);
                    }
                }

                return newAppointment;
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them)
                Console.WriteLine($"Error adding/updating appointment: {ex.Message}");
                return null;
            }
        }

        public async Task DeleteAppointment(int id)
        {
            try
            {
                var appointmentToRemove = Appointments.FirstOrDefault(a => a.Id == id);

                if (appointmentToRemove != null)
                {
                    Appointments.Remove(appointmentToRemove);
                    await new WebRequestHandler().Delete($"/Appointment/{id}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them)
                Console.WriteLine($"Error deleting appointment: {ex.Message}");
            }
        }
    }
}