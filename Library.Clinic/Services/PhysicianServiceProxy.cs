using Library.Clinic.DTO;
using Library.Clinic.Models;
using Newtonsoft.Json;
using PP.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
namespace Library.Clinic.Services
{
    public class PhysicianServiceProxy
    {
        private static readonly object _lock = new object();
        public static PhysicianServiceProxy Current
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new PhysicianServiceProxy();
                    }
                }
                return instance;
            }
        }

        private static PhysicianServiceProxy? instance;
        private PhysicianServiceProxy()
        {
            instance = null;
            physicians = new List<PhysicianDTO>();
            var physiciansData = new WebRequestHandler().Get("/Physician").Result;
            Physicians = JsonConvert.DeserializeObject<List<PhysicianDTO>>(physiciansData) ?? new List<PhysicianDTO>();
        }

        private List<PhysicianDTO> physicians;
        public List<PhysicianDTO> Physicians
        {
            get
            {
                return physicians;
            }
            private set
            {
                if (physicians != value)
                {
                    physicians = value;
                }
            }
        }

        public async Task<List<PhysicianDTO>> Search(string query)
        {
            var physiciansPayload = await new WebRequestHandler()
                .Post("/Physician/Search", new Query(query));

            Physicians = JsonConvert.DeserializeObject<List<PhysicianDTO>>(physiciansPayload)
                ?? new List<PhysicianDTO>();

            return Physicians;
        }

        public async Task<PhysicianDTO?> AddOrUpdatePhysician(PhysicianDTO physician)
        {
            if (physician != null)
            {
                if (string.IsNullOrEmpty(physician.Id) || physician.Id == "0")
                {
                    Physicians.Add(physician);
                    await new WebRequestHandler().Post("/Physician/Add", physician);
                }
                else
                {
                    var existingPhysician = Physicians.FirstOrDefault(p => p.Id == physician.Id);
                    if (existingPhysician != null)
                    {
                        Physicians[Physicians.IndexOf(existingPhysician)] = physician;
                        await new WebRequestHandler().Post("/Physician/Update", physician);
                    }
                    else
                    {
                        Physicians.Add(physician);
                        await new WebRequestHandler().Post("/Physician/Add", physician);
                    }
                }
                await RefreshPhysicians();
                return physician;
            }
            await RefreshPhysicians();
            return null;
        }

        public async Task RefreshPhysicians()
        {
            var physiciansData = await new WebRequestHandler().Get("/Physician");
            Physicians = JsonConvert.DeserializeObject<List<PhysicianDTO>>(physiciansData) ?? new List<PhysicianDTO>();
        }

        public async void DeletePhysician(ObjectId id)
        {
            var physicianToRemove = Physicians.FirstOrDefault(p => p.Id == id.ToString());

            if (physicianToRemove != null)
            {
                Physicians.Remove(physicianToRemove);
                await new WebRequestHandler().Delete($"/Physician/{id}");
            }
            await RefreshPhysicians();
        }
    }
}
