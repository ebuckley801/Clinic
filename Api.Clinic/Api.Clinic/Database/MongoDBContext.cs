using Library.Clinic.Models;
using MongoDB.Driver;
using MongoDB.Bson;
namespace Api.Clinic.Database
{
    public class MongoDBContext
    {
        private string connectionString = "mongodb+srv://ebuckley801:35rfy98r@clinic.jun7m.mongodb.net/?retryWrites=true&w=majority&appName=Clinic";

        public MongoDBContext() { }

        public async Task<IEnumerable<Patient>> GetPatients()
        {
            var returnVal = new List<Patient>();
            using (var conn = new MongoClient(connectionString))
            {
                var database = conn.GetDatabase("Clinic");
                var collection = database.GetCollection<Patient>("Patients");
                var patients = await collection.Find(FilterDefinition<Patient>.Empty).ToListAsync();
                return patients;
            }
        }

        public async Task<Patient> GetPatient(ObjectId id)
        {
            using (var conn = new MongoClient(connectionString))
            {
                var database = conn.GetDatabase("Clinic");
                var collection = database.GetCollection<Patient>("Patients");
                return await collection.Find(new BsonDocument("_id", id)).FirstOrDefaultAsync();
            }
        }
        
        public async Task<Patient> AddOrUpdatePatient(Patient patient)
        {
            using (var conn = new MongoClient(connectionString))
            {
                var database = conn.GetDatabase("Clinic");
                var collection = database.GetCollection<Patient>("Patients");
                if(patient.Id == null)
                {
                    await collection.InsertOneAsync(patient);
                }
                else
                {
                    await collection.ReplaceOneAsync(FilterDefinition<Patient>.Empty, patient);
                }
            }
            return patient;
        }

        public async Task DeletePatient(ObjectId id)
        {
            using (var conn = new MongoClient(connectionString))
            {
                var database = conn.GetDatabase("Clinic");
                var collection = database.GetCollection<Patient>("Patients");
                await collection.DeleteOneAsync(FilterDefinition<Patient>.Empty);
            }
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            var returnVal = new List<Appointment>();
            using (var conn = new MongoClient(connectionString))
            {
                var database = conn.GetDatabase("Clinic");
                var collection = database.GetCollection<Appointment>("Appointments");
                var appointments = await collection.Find(FilterDefinition<Appointment>.Empty).ToListAsync();
                return appointments;
            }
        }   

        public async Task DeleteAppointment(int id)
        {
            using (var conn = new MongoClient(connectionString))
            {
                var database = conn.GetDatabase("Clinic");
                var collection = database.GetCollection<Appointment>("Appointments");
                await collection.DeleteOneAsync(FilterDefinition<Appointment>.Empty);
            }
        }

        public async Task UpdateAppointment(Appointment app)
        {
            using (var conn = new MongoClient(connectionString))
            {
                var database = conn.GetDatabase("Clinic");
                var collection = database.GetCollection<Appointment>("Appointments");
                await collection.ReplaceOneAsync(FilterDefinition<Appointment>.Empty, app);
            }
        }

        public async Task<Appointment> CreateAppointment(Appointment app)
        {
            using (var conn = new MongoClient(connectionString))
            {
                var database = conn.GetDatabase("Clinic");
                var collection = database.GetCollection<Appointment>("Appointments");
                await collection.InsertOneAsync(app);
            }
            return app;
        }

    }
}