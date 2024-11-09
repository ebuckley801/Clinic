using Api.Clinic.Enterprise;
using Library.Clinic.DTO;
using Library.Clinic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Clinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<AppointmentController> _logger;

        public AppointmentController(ILogger<AppointmentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<AppointmentDTO> Get()
        {
            return new AppointmentEC().Appointments;
        }

        [HttpGet("{id}")]
        public AppointmentDTO? GetById(int id)
        {
            return new AppointmentEC().GetById(id);
        }

        [HttpDelete("{id}")]
        public AppointmentDTO? Delete(int id)
        {
            return new AppointmentEC().Delete(id);
        }

        [HttpPost("Search")]
        public List<AppointmentDTO> Search([FromBody] Query q)
        {
            return new AppointmentEC().Search(q?.Content ?? string.Empty)?.ToList() ?? new List<AppointmentDTO>();
        }

        [HttpPost]
        public Appointment? AddOrUpdate([FromBody] AppointmentDTO? appointment)
        {
            return new AppointmentEC().AddOrUpdate(appointment);
        }
    }
}
