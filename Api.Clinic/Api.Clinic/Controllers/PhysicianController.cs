using Api.Clinic.Enterprise;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Api.Clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhysicianController : ControllerBase
    {
        private readonly ILogger<PhysicianController> _logger;
        private readonly PhysicianEC _physicianEC;

        public PhysicianController(ILogger<PhysicianController> logger)
        {
            _logger = logger;
            _physicianEC = new PhysicianEC();
        }

        [HttpGet]
        public async Task<IEnumerable<PhysicianDTO>> Get()
        {
            return await _physicianEC.GetPhysicians();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhysicianDTO?>> GetById(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format.");
            }
            
            var physician = await _physicianEC.GetById(objectId);
            if (physician == null)
            {
                return NotFound();
            }
            return Ok(physician);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PhysicianDTO?>> Delete(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format.");
            }

            var deletedPhysician = await _physicianEC.Delete(objectId);
            if (deletedPhysician == null)
            {
                return NotFound();
            }
            return Ok(deletedPhysician);
        }

        [HttpPost("Search")]
        public async Task<List<PhysicianDTO>> Search([FromBody] Query q)
        {
            return (await _physicianEC.Search(q?.Content ?? string.Empty)).ToList();
        }

        [HttpPost("Add")]
        public async Task<ActionResult<PhysicianDTO?>> Add([FromBody] PhysicianDTO? physician)
        {
            return Ok(await _physicianEC.AddPhysician(new Physician(physician)));
        }

        [HttpPost("Update")]
        public async Task<ActionResult<PhysicianDTO?>> Update([FromBody] PhysicianDTO? physician)
        {
            return Ok(await _physicianEC.UpdatePhysician(new Physician(physician)));
        }
    }
}