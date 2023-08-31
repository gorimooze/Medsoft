using AutoMapper;
using Medsoft.Dto;
using Medsoft.Interfaces;
using Medsoft.Models;
using Medsoft.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Medsoft.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class PatientStateController : Controller
    {
        private readonly IPatientStateRepository _patientStateRepository;
        private readonly IMapper _mapper;

        public PatientStateController(IPatientStateRepository patientStateRepository, IMapper mapper)
        {
            _patientStateRepository = patientStateRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PatientState>))]
        public IActionResult GetAll()
        {
            var states = _mapper.Map<List<PatientStateDto>>(_patientStateRepository.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(states);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] PatientStateDto stateCreate)
        {
            if (stateCreate == null)
                return BadRequest(ModelState);

            var state = _patientStateRepository.GetAll()
                .Where(c => c.Name.Trim().ToUpper() == stateCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (state != null)
            {
                ModelState.AddModelError("", "State already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stateMap = _mapper.Map<PatientState>(stateCreate);

            if (!_patientStateRepository.CreatePatientState(stateMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{stateId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int stateId, [FromBody] PatientStateDto updatedState)
        {
            if (updatedState == null)
                return BadRequest(ModelState);

            if (stateId != updatedState.Id)
                return BadRequest(ModelState);

            if (!_patientStateRepository.PatientStateExists(stateId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var stateMap = _mapper.Map<PatientState>(updatedState);

            if (!_patientStateRepository.UpdatePatientState(stateMap))
            {
                ModelState.AddModelError("", "Something went wrong updating state");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{stateId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int stateId)
        {
            if (!_patientStateRepository.PatientStateExists(stateId))
            {
                return NotFound();
            }

            var stateToDelete = _patientStateRepository.GetById(stateId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_patientStateRepository.DeletePatientState(stateToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting state");
            }

            return NoContent();
        }
    }
}