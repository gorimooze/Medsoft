using AutoMapper;
using Medsoft.Interfaces;
using Medsoft.Models;
using Microsoft.AspNetCore.Mvc;
using Medsoft.Dto;

namespace Medsoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : Controller
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientController(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Patient>))]
        public IActionResult GetAll()
        {
            var patients = _mapper.Map<List<PatientDto>>(_patientRepository.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(patients);
        }

        [HttpGet("{patientId}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(400)]
        public IActionResult GetPatient(long patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            var patient = _mapper.Map<PatientDto>(_patientRepository.GetPatientById(patientId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(patient);
        }

        [HttpGet("{idnp}")]
        [ProducesResponseType(200, Type = typeof(Patient))]
        [ProducesResponseType(400)]
        public IActionResult GetPatient(decimal idnp)
        {
            var patient = _mapper.Map<PatientDto>(_patientRepository.GetPatientByIDNP(idnp));

            if (patient == null)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(patient);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePatient([FromBody] PatientDto patientCreate)
        {
            if (patientCreate == null)
                return BadRequest(ModelState);

            var patient = _patientRepository.GetAll()
                .Where(p => p.IDNP == patientCreate.IDNP).FirstOrDefault();

            if (patient != null)
            {
                ModelState.AddModelError("", "Patient already exists");
                return StatusCode(422, ModelState);
            }
                
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patientMap = _mapper.Map<Patient>(patientCreate);

            if (!_patientRepository.CreatePatient(patientMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{patientId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePatient(long patientId, [FromBody] PatientDto updatedPatient)
        {
            if (updatedPatient == null)
                return BadRequest(ModelState);

            if (patientId != updatedPatient.Id)
                return BadRequest(ModelState);

            if (!_patientRepository.PatientExists(patientId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var patientMap = _mapper.Map<Patient>(updatedPatient);

            if (!_patientRepository.UpdatePatient(patientMap))
            {
                ModelState.AddModelError("", "Something went wrong updating patient");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{patientId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePatient(long patientId)
        {
            if (!_patientRepository.PatientExists(patientId))
            {
                return NotFound();
            }

            var patientToDelete = _patientRepository.GetPatientById(patientId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_patientRepository.DeletePatient(patientToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting patient");
            }

            return NoContent();
        }
    }
}
