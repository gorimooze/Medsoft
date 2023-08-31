using AutoMapper;
using Medsoft.Dto;
using Medsoft.Interfaces;
using Medsoft.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace Medsoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SexController : Controller
    {
        private readonly ISexRepository _sexRepository;
        private readonly IMapper _mapper;

        public SexController(ISexRepository sexRepository, IMapper mapper)
        {
            _sexRepository = sexRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sex>))]
        public IActionResult GetAll()
        { 
            var sexes = _mapper.Map<List<SexDto>>(_sexRepository.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(sexes);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] SexDto sexCreate)
        {
            if (sexCreate == null)
                return BadRequest(ModelState);

            var sex = _sexRepository.GetAll()
                .Where(c => c.Name.Trim().ToUpper() == sexCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (sex != null)
            {
                ModelState.AddModelError("", "Sex already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sexMap = _mapper.Map<Sex>(sexCreate);

            if (!_sexRepository.CreateSex(sexMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{sexId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int sexId, [FromBody] SexDto updatedSex)
        {
            if (updatedSex == null)
                return BadRequest(ModelState);

            if (sexId != updatedSex.Id)
                return BadRequest(ModelState);

            if (!_sexRepository.SexExists(sexId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var sexMap = _mapper.Map<Sex>(updatedSex);

            if (!_sexRepository.UpdateSex(sexMap))
            {
                ModelState.AddModelError("", "Something went wrong updating sex");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{sexId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int sexId)
        {
            if (!_sexRepository.SexExists(sexId))
            {
                return NotFound();
            }

            var sexToDelete = _sexRepository.GetById(sexId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_sexRepository.DeleteSex(sexToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting sex");
            }

            return NoContent();
        }
    }
}
