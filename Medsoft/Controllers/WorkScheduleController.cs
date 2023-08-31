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
    public class WorkScheduleController : Controller
    {
        private readonly IWorkScheduleRepository _workScheduleRepository;
        private readonly IMapper _mapper;

        public WorkScheduleController(IWorkScheduleRepository workScheduleRepository, IMapper mapper)
        {
            _workScheduleRepository = workScheduleRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WorkSchedule>))]
        public IActionResult GetAll()
        {
            var workSchedules = _mapper.Map<List<WorkScheduleDto>>(_workScheduleRepository.GetAll());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(workSchedules);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] WorkScheduleDto workScheduleCreate)
        {
            if (workScheduleCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var workScheduleMap = _mapper.Map<WorkSchedule>(workScheduleCreate);

            if (!_workScheduleRepository.CreateWorkSchedule(workScheduleMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{workScheduleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Update(int workScheduleId, [FromBody] WorkScheduleDto updatedWorkSchedule)
        {
            if (updatedWorkSchedule == null)
                return BadRequest(ModelState);

            if (workScheduleId != updatedWorkSchedule.Id)
                return BadRequest(ModelState);

            //if (!_workScheduleRepository.WorkScheduleExists(workScheduleId))
            //    return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var workScheduleMap = _mapper.Map<WorkSchedule>(updatedWorkSchedule);

            if (!_workScheduleRepository.UpdateWorkSchedule(workScheduleMap))
            {
                ModelState.AddModelError("", "Something went wrong updating schedule");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{workScheduleId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Delete(int workScheduleId)
        {
            if (!_workScheduleRepository.WorkScheduleExists(workScheduleId))
            {
                return NotFound();
            }

            var workScheduleToDelete = _workScheduleRepository.GetById(workScheduleId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_workScheduleRepository.DeleteWorkSchedule(workScheduleToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting schedule");
            }

            return NoContent();
        }
    }
}
