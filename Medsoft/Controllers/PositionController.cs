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
    public class PositionController : Controller
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IMapper _mapper;

        public PositionController(IPositionRepository positionRepository, IMapper mapper)
        {
            _positionRepository = positionRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Position>))]
        public IActionResult GetAll()
        {
            var positions = _mapper.Map<List<PositionDto>>(_positionRepository.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(positions);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] PositionDto positionCreate)
        {
            if (positionCreate == null)
                return BadRequest(ModelState);

            var position = _positionRepository.GetAll()
                .Where(c => c.Name.Trim().ToUpper() == positionCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (position != null)
            {
                ModelState.AddModelError("", "Position already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var positionMap = _mapper.Map<Position>(positionCreate);

            if (!_positionRepository.CreatePosition(positionMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{positionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int positionId, [FromBody] PositionDto updatedPosition)
        {
            if (updatedPosition == null)
                return BadRequest(ModelState);

            if (positionId != updatedPosition.Id)
                return BadRequest(ModelState);

            if (!_positionRepository.PositionExists(positionId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var positionMap = _mapper.Map<Position>(updatedPosition);

            if (!_positionRepository.UpdatePosition(positionMap))
            {
                ModelState.AddModelError("", "Something went wrong updating position");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{positionId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int positionId)
        {
            if (!_positionRepository.PositionExists(positionId))
            {
                return NotFound();
            }

            var positionToDelete = _positionRepository.GetById(positionId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_positionRepository.DeletePosition(positionToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting position");
            }

            return NoContent();
        }
    }
}
