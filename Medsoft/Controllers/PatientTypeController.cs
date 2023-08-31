using AutoMapper;
using Medsoft.Dto;
using Medsoft.Interfaces;
using Medsoft.Models;
using Medsoft.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Medsoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientTypeController : Controller
    {
        private readonly IPatientTypeRepository _patientTypeRepository;
        private readonly IMapper _mapper;

        public PatientTypeController(IPatientTypeRepository patientTypeRepository, IMapper mapper)
        {
            _patientTypeRepository = patientTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatientType>))]
        public IActionResult GetAll()
        {
            var patientTypes = _mapper.Map<List<PatientTypeDto>>(_patientTypeRepository.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(patientTypes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] PatientTypeDto patientTypeCreate)
        {
            if (patientTypeCreate == null)
                return BadRequest(ModelState);

            var sex = _patientTypeRepository.GetAll()
                .Where(c => c.Name.Trim().ToUpper() == patientTypeCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (sex != null)
            {
                ModelState.AddModelError("", "Patient type already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patientTypeMap = _mapper.Map<PatientType>(patientTypeCreate);

            if (!_patientTypeRepository.CreatePatientType(patientTypeMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{patientTypeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int patientTypeId, [FromBody] PatientTypeDto updatedPatientType)
        {
            if (updatedPatientType == null)
                return BadRequest(ModelState);

            if (patientTypeId != updatedPatientType.Id)
                return BadRequest(ModelState);

            if (!_patientTypeRepository.PatientTypeExists(patientTypeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var patientTypeMap = _mapper.Map<PatientType>(updatedPatientType);

            if (!_patientTypeRepository.UpdatePatientType(patientTypeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating patient type");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{patientTypeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int patientTypeId)
        {
            if (!_patientTypeRepository.PatientTypeExists(patientTypeId))
            {
                return NotFound();
            }

            var ptToDelete = _patientTypeRepository.GetById(patientTypeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_patientTypeRepository.DeletePatientType(ptToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting patient type");
            }

            return NoContent();
        }
    }
}
