using Api.Clinic.Database;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using MongoDB.Driver;
using MongoDB.Bson;
namespace Api.Clinic.Enterprise
{
    public class PatientEC
    {
        public PatientEC() { }

        private MongoDBContext _mongoDBContext = new MongoDBContext();

        public async Task<IEnumerable<PatientDTO>> GetPatients()
        {
            var patients = await _mongoDBContext.GetPatients();
            return patients.Take(100).Select(p => new PatientDTO(p));
        }

        public async Task<IEnumerable<PatientDTO>> Search(string query)
        {
            return (await _mongoDBContext.GetPatients())
                .Where(p => p.Name?.ToUpper()
                    .Contains(query?.ToUpper() ?? string.Empty) ?? false)
                .Select(p => new PatientDTO(p));
        }

        public async Task<PatientDTO?> GetById(ObjectId id)
        {
            var patient = await _mongoDBContext.GetPatient(id);
            if(patient != null)
            {
                return new PatientDTO(patient);
            }

            return null;
        }

        public async Task<PatientDTO?> Delete(ObjectId id)
        {
            var patientToDelete = await _mongoDBContext.GetPatient(id);
            if (patientToDelete != null)
            {
                await _mongoDBContext.DeletePatient(id);
                return new PatientDTO(patientToDelete);
            }

            return null;
        }

        public async Task<Patient?> AddPatient(Patient patient)
        {
            return await _mongoDBContext.AddPatient(patient);
        }

        public async Task<Patient?> UpdatePatient(Patient patient)
        {
            return await _mongoDBContext.UpdatePatient(patient);
        }
    }
}