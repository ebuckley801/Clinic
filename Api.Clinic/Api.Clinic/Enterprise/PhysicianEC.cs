using Api.Clinic.Database;
using Library.Clinic.DTO;
using Library.Clinic.Models;

namespace Api.Clinic.Enterprise
{
    public class PhysicianEC
    {
        public PhysicianEC() { }

        public IEnumerable<PhysicianDTO> Physicians
        {
            get
            {
                return FakePhysicianDatabase.Physicians.Take(100).Select(p => new PhysicianDTO(p));
            }
        }

        public IEnumerable<PhysicianDTO>? Search(string query)
        {
            return FakePhysicianDatabase.Physicians
                .Where(p => p.Name?.ToUpper()
                    .Contains(query?.ToUpper() ?? string.Empty) ?? false)
                .Select(p => new PhysicianDTO(p));
        }

        public PhysicianDTO? GetById(int id)
        {
            var physician = FakePhysicianDatabase
                .Physicians
                .FirstOrDefault(p => p.Id == id);
            if (physician != null)
            {
                return new PhysicianDTO(physician);
            }

            return null;
        }

        public PhysicianDTO? Delete(int id)
        {
            var physicianToDelete = FakePhysicianDatabase.Physicians.FirstOrDefault(p => p.Id == id);
            if (physicianToDelete != null)
            {
                FakePhysicianDatabase.Physicians.Remove(physicianToDelete);
                return new PhysicianDTO(physicianToDelete);
            }

            return null;
        }

        public Physician? AddOrUpdate(PhysicianDTO? physician)
        {
            if (physician == null)
            {
                return null;
            }
            return FakePhysicianDatabase.AddOrUpdatePhysician(new Physician(physician));
        }
    }
}
