using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel;

namespace Library.Clinic.Services
{
    public class PatientServiceProxy
    {
        private static object _lock = new object();
        public static PatientServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PatientServiceProxy();
                    }
                }
                return instance;
            }
        }

        private static PatientServiceProxy? instance;
        private PatientServiceProxy() 
        {
            instance = null;
            patients = new List<PatientDTO>();
            var patientsData = new WebRequestHandler().Get("/Patient").Result;
            Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsData) ?? new List<PatientDTO>();
        }

        private List<PatientDTO> patients;
        public List<PatientDTO> Patients {
            get {
                return patients;
            }

            private set
            {
                if (patients != value)
                {
                    patients = value;
                }
            }
        }

        public async Task<List<PatientDTO>> Search(string query) {
            var patientsPayload = await new WebRequestHandler()
                .Post("/Patient/Search", new Query(query));

            Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsPayload)
                ?? new List<PatientDTO>();

            return Patients;
        }

        public async Task<PatientDTO?> AddOrUpdatePatient(PatientDTO patient)
        {
            if (patient != null)
            {
                // For new patients
                if (string.IsNullOrEmpty(patient.Id) || patient.Id == "0")
                {
                    Patients.Add(patient);
                    await new WebRequestHandler().Post("/Patient/Add", patient);
                }
                // For existing patients
                else
                {
                    var existingPatient = Patients.FirstOrDefault(p => p.Id == patient.Id);
                    if (existingPatient != null)
                    {
                        Patients[Patients.IndexOf(existingPatient)] = patient;
                        await new WebRequestHandler().Post("/Patient/Update", patient);
                    }
                    else
                    {
                        Patients.Add(patient);
                        await new WebRequestHandler().Post("/Patient/Add", patient);
                    }
                }
                await RefreshPatients();
                return patient;
            }
            await RefreshPatients();
            return null;
        }

        public async Task RefreshPatients()
        {
            var patientsData = await new WebRequestHandler().Get("/Patient");
            Patients = JsonConvert.DeserializeObject<List<PatientDTO>>(patientsData) ?? new List<PatientDTO>();
        }

        public async void DeletePatient(ObjectId id) {
            var patientToRemove = Patients.FirstOrDefault(p => p.Id == id.ToString());

            if (patientToRemove != null)
            {
                Patients.Remove(patientToRemove);
                await new WebRequestHandler().Delete($"/Patient/{id}");
            }
            await RefreshPatients();
        }
    }
}
