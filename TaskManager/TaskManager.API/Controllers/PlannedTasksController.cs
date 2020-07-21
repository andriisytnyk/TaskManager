using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.Core.Commands.PlannedTaskCommands;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.Core.Specifications.PlannedTasksSpecifications;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlannedTasksController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public IRepository<PlannedTask> PlannedTaskRepository { get; set; }

        public PlannedTasksController(IRepository<PlannedTask> plannedTaskRepository, ICommandBus commandBus)
        {
            PlannedTaskRepository = plannedTaskRepository;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlannedTasks()
        {
            try
            {
                var plannedTaskWithDependenciesSpecification = new PlannedTaskWithDependenciesSpecification();

                return Ok(await PlannedTaskRepository.GetList(plannedTaskWithDependenciesSpecification));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetPlannedTask(int id)
        {
            try
            {
                var plannedTaskByIdSpecification = new PlannedTaskByIdSpecification(id);

                return Ok((await PlannedTaskRepository.GetList(plannedTaskByIdSpecification)).SingleOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlannedTask([FromBody] PlannedTaskDTO plannedTaskDTO)
        {
            try
            {
                var command = new CreatePlannedTaskCommand(
                    Guid.NewGuid(),
                    plannedTaskDTO.Name,
                    plannedTaskDTO.Description,
                    plannedTaskDTO.Status,
                    plannedTaskDTO.StartDateTime,
                    plannedTaskDTO.FinishDateTime,
                    plannedTaskDTO.Estimation,
                    plannedTaskDTO.Requirement,
                    plannedTaskDTO.Frequency,
                    plannedTaskDTO.SubTasks?.Select(st => new Core.Commands.SubTaskApplicationModel(
                        st.Id,
                        st.Name,
                        st.Description,
                        st.Status)));

                await _commandBus.Execute(command);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePlannedTask([FromBody] PlannedTaskDTO plannedTaskDTO)
        {
            try
            {
                var command = new UpdatePlannedTaskCommand(
                    Guid.NewGuid(),
                    plannedTaskDTO.Id,
                    plannedTaskDTO.Name,
                    plannedTaskDTO.Description,
                    plannedTaskDTO.Status,
                    plannedTaskDTO.StartDateTime,
                    plannedTaskDTO.FinishDateTime,
                    plannedTaskDTO.Estimation,
                    plannedTaskDTO.Requirement,
                    plannedTaskDTO.Frequency,
                    plannedTaskDTO.SubTasks?.Select(st => new Core.Commands.SubTaskApplicationModel(
                        st.Id,
                        st.Name,
                        st.Description,
                        st.Status)));

                await _commandBus.Execute(command);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeletePlannedTask(int id)
        {
            try
            {
                var command = new DeletePlannedTaskCommand(Guid.NewGuid(), id);

                await _commandBus.Execute(command);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
