using Library.Clinic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using Api.Clinic.Database;

namespace Api.ToDoApplication.Persistence
{
    public class Filebase
    {
        private string _root;
        private string _patientRoot;
        private Filebase _instance;
        public Filebase Current
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }
                return _instance;
            }
        }
        private Filebase()
        {
            _root = @"C:\temp";
            _patientRoot = $"{_root}\\Patients";
        }
        public async Task<Patient> AddOrUpdate(Patient patient)
        {
            var mongoDBContext = new MongoDBContext();
            //set up a new Id if one doesn't already exist
            if(patient.Id == null)
            {
                patient.Id = (await mongoDBContext.AddOrUpdatePatient(patient)).Id;
            }   
            //go to the right place
            string path = $"{_patientRoot}\\{patient.Id}.json";
            
            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }
            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(patient));
            //return the item, which now has an id
            return patient;
        }
        
        public List<Patient> Patients
        {
            get
            {
                var root = new DirectoryInfo(_patientRoot);
                var _patients = new List<Patient>();
                foreach(var patientFile in root.GetFiles())
                {
                    var patient = JsonConvert
                        .DeserializeObject<Patient>
                        (File.ReadAllText(patientFile.Name));
                    if(patient != null)
                    {
                        _patients.Add(patient);
                    }
                }
                return _patients;
            }
        }
        public bool Delete(ObjectId id)
        {
            string path = $"{_patientRoot}\\{id.ToString()}.json";
            if(File.Exists(path))
            {
                File.Delete(path);
            }
            return true;
        }
    }
   
}