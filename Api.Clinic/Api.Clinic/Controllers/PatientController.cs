using Api.Clinic.Enterprise;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;


namespace Api.Clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly ILogger<PatientController> _logger;
        private readonly PatientEC _patientEC;
        private readonly IConfiguration _configuration;

        public PatientController(ILogger<PatientController> logger)
        {
            _logger = logger;   
            _patientEC = new PatientEC();
        }

        /// <summary>
        /// Retrieves all patients.
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<PatientDTO>> Get()
        {
            var patients = await _patientEC.GetPatients();
            return patients;
        }

        /// <summary>
        /// Retrieves a patient by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetById(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid ID format.");
            }

            var patient = await _patientEC.GetById(objectId);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        /// <summary>
        /// Deletes a patient by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientDTO>> Delete(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId objectId))
            {
                return BadRequest("Invalid ID format.");
            }

            var deletedPatient = await _patientEC.Delete(objectId);
            if (deletedPatient == null)
            {
                return NotFound();
            }

            return Ok(deletedPatient);
        }

        /// <summary>
        /// Searches for patients based on a query.
        /// </summary>
        [HttpPost("Search")]
        public async Task<ActionResult<List<PatientDTO>>> Search([FromBody] Query q)
        {
            var results = await _patientEC.Search(q?.Content ?? string.Empty);
            return Ok(results);
        }

        /// <summary>
        /// Adds a new patient or updates an existing one.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<PatientDTO>> AddOrUpdate([FromBody] PatientDTO? patientDto)
        {
            if (patientDto == null)
            {
                return BadRequest("Patient data is required.");
            }

            // Convert DTO to internal model
            var patient = new Patient(patientDto);

            // Ensure the ID is a valid ObjectId
            if (!ObjectId.TryParse(patientDto.Id, out ObjectId objectId))
            {
                patient.Id = ObjectId.GenerateNewId();
            }
            else
            {
                patient.Id = objectId;
            }
            var result = await _patientEC.AddOrUpdate(patientDto);
            return Ok(result);
        }
    }
}