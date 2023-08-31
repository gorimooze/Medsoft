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
    public class AdmissionController : Controller
    {
        private readonly IAdmissionRepository _admissionRepository;
        private readonly IMapper _mapper;

        public AdmissionController(IAdmissionRepository admissionRepository, IMapper mapper)
        {
            _admissionRepository = admissionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Admission>))]
        public IActionResult GetAll()
        {
            var admission = _mapper.Map<List<AdmissionDto>>(_admissionRepository.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(admission);
        }

        [HttpGet("{admissionId}")]
        [ProducesResponseType(200, Type = typeof(Admission))]
        [ProducesResponseType(400)]
        public IActionResult GetAdmission(int admissionId)
        {
            if (!_admissionRepository.AdmissionExists(admissionId))
                return NotFound();

            var admission = _mapper.Map<AdmissionDto>(_admissionRepository.GetById(admissionId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(admission);
        }

        [HttpGet("{patientId}")]
        [ProducesResponseType(200, Type = typeof(Admission))]
        [ProducesResponseType(400)]
        public IActionResult GetByPatient(long patientId)
        {
            var admissions = _mapper.Map<List<AdmissionDto>>(_admissionRepository.GetAdmissionByPatient(patientId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(admissions);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] AdmissionDto admissionCreate)
        {
            if (admissionCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var admissionMap = _mapper.Map<Admission>(admissionCreate);

            if (!_admissionRepository.CreateAdmission(admissionMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{admissionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int admissionId, [FromBody] AdmissionDto updatedAdmission)
        {
            if (updatedAdmission == null)
                return BadRequest(ModelState);

            if (admissionId != updatedAdmission.Id)
                return BadRequest(ModelState);

            if (!_admissionRepository.AdmissionExists(admissionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var admissionMap = _mapper.Map<Admission>(updatedAdmission);

            if (!_admissionRepository.UpdateAdmission(admissionMap))
            {
                ModelState.AddModelError("", "Something went wrong updating admission");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{admissionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int admissionId)
        {
            if (!_admissionRepository.AdmissionExists(admissionId))
            {
                return NotFound();
            }

            var admissionToDelete = _admissionRepository.GetById(admissionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_admissionRepository.DeleteAdmission(admissionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting admission");
            }

            return NoContent();
        }
    }
}
