using Api.Clinic.Database;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Api.Clinic.Enterprise
{
    public class PhysicianEC
    {
        private MongoDBContext _mongoDBContext = new MongoDBContext();

        public async Task<IEnumerable<PhysicianDTO>> GetPhysicians()
        {
            var physicians = await _mongoDBContext.GetPhysicians();
            return physicians.Take(100).Select(p => new PhysicianDTO(p));
        }

        public async Task<IEnumerable<PhysicianDTO>> Search(string query)
        {
            return (await _mongoDBContext.GetPhysicians())
                .Where(p => p.Name?.ToUpper()
                    .Contains(query?.ToUpper() ?? string.Empty) ?? false)
                .Select(p => new PhysicianDTO(p));
        }

        public async Task<PhysicianDTO?> GetById(ObjectId id)
        {
            var physician = await _mongoDBContext.GetPhysician(id);
            if (physician != null)
            {
                return new PhysicianDTO(physician);
            }

            return null;
        }

        public async Task<PhysicianDTO?> Delete(ObjectId id)
        {
            var physicianToDelete = await _mongoDBContext.GetPhysician(id);
            if (physicianToDelete != null)
            {
                await _mongoDBContext.DeletePhysician(id);
                return new PhysicianDTO(physicianToDelete);
            }

            return null;
        }

        public async Task<Physician?> AddPhysician(Physician physician)
        {
            return await _mongoDBContext.AddPhysician(physician);
        }

        public async Task<Physician?> UpdatePhysician(Physician physician)
        {
            return await _mongoDBContext.UpdatePhysician(physician);
        }
    }
}
