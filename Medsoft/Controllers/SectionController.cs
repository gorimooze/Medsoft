using AutoMapper;
using Medsoft.Dto;
using Medsoft.Interfaces;
using Medsoft.Models;
using Medsoft.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace Medsoft.Controllers
{
    [Route("api/{controller}")]
    [ApiController]
    public class SectionController : Controller
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IMapper _mapper;

        public SectionController(ISectionRepository sectionRepository, IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Section>))]
        public IActionResult GetAll()
        {
            var sections = _mapper.Map<List<SectionDto>>(_sectionRepository.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sections);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] SectionDto sectionCreate)
        {
            if (sectionCreate == null)
                return BadRequest(ModelState);

            var section = _sectionRepository.GetAll()
                .Where(c => c.Name.Trim().ToUpper() == sectionCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (section != null)
            {
                ModelState.AddModelError("", "Section already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var sectionMap = _mapper.Map<Section>(sectionCreate);

            if (!_sectionRepository.CreateSection(sectionMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{sectionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int sectionId, [FromBody] SectionDto sectionUpdated)
        {
            if (sectionUpdated == null)
                return BadRequest(ModelState);

            if (sectionId != sectionUpdated.Id)
                return BadRequest(ModelState);

            if (!_sectionRepository.SectionExists(sectionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var sectionMap = _mapper.Map<Section>(sectionUpdated);

            if (!_sectionRepository.UpdateSection(sectionMap))
            {
                ModelState.AddModelError("", "Something went wrong updating section");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{sectionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int sectionId)
        {
            if (!_sectionRepository.SectionExists(sectionId))
            {
                return NotFound();
            }

            var sectionToDelete = _sectionRepository.GetById(sectionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_sectionRepository.DeleteSection(sectionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting section");
            }

            return NoContent();
        }
    }
}
