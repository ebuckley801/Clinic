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
        [HttpPost("Add")]
        public async Task<ActionResult<PatientDTO>> Add([FromBody] PatientDTO? patientDto)
        {
            var patient = new Patient(patientDto);
            var result = await _patientEC.AddPatient(patient);
            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<ActionResult<PatientDTO>> Update([FromBody] PatientDTO? patientDto)
        {
            var patient = new Patient(patientDto);
            var result = await _patientEC.UpdatePatient(patient);
            return Ok(result);
        }
    }
}