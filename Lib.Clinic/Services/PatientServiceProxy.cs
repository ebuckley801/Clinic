using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Clinic.Models;


namespace Library.Clinic.Services
{
    public class PatientServiceProxy
    {

        //Singleton pattern, can only have one instance of the class; i.e. why it is private
        private static object _lock = new object();

        private static PatientServiceProxy? instance;
        private PatientServiceProxy()
        {
            //Private constructor to prevent instantiation
            instance = null;

        }

        public static PatientServiceProxy Current
        {
            get
            {
                //prevents multiple threads from accessing the instance at the same time
                //Want to identify smallest thing to lock, to prevent deadlocks
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
        public List<Patient> Patients { get; private set; } = new List<Patient>();

        public  int LastKey
        {
            get
            {
                if(Patients.Any())
                {
                    return Patients.Max(x => x.Id);
                }
                return 0;
            }
        }


        public void AddPatient(Patient patient)
        {
            if(patient.Id <= 0)
            {
                patient.Id = LastKey + 1;
            }
            Patients.Add(patient);
        }

        public void DeletePatient(int id)
        {
            var patientToRemove = Patients.FirstOrDefault(x => x.Id == id);
            if (patientToRemove != null) {
                Patients.Remove(patientToRemove);
            }
        }

        public Patient FindPatient(int id)
        {
            var patientToFind = Patients.FirstOrDefault(x => x.Id == id);
            if (patientToFind != null)
            {
                return patientToFind;
            }
            return null;
        }

    }
}
