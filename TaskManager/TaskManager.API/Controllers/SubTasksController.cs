using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.Core.Commands.SubTaskCommands;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.Core.Specifications.SubTasksSpecifications;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SubTasksController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public IRepository<SubTask> SubTaskRepository { get; set; }

        public SubTasksController(IRepository<SubTask> subTaskRepository, ICommandBus commandBus)
        {
            SubTaskRepository = subTaskRepository;
            _commandBus = commandBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubTasks()
        {
            try
            {
                var subTasksAllSpecification = new SubTasksAllSpecification();

                return Ok(await SubTaskRepository.GetList(subTasksAllSpecification));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubTask(int id)
        {
            try
            {
                var subTaskByIdSpecification = new SubTaskByIdSpecification(id);

                return Ok((await SubTaskRepository.GetList(subTaskByIdSpecification)).SingleOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubTask([FromBody] SubTaskDTO subTaskDTO)
        {
            try
            {
                var command = new CreateSubTaskCommand(
                    Guid.NewGuid(),
                    subTaskDTO.Name,
                    subTaskDTO.Description,
                    subTaskDTO.Status);

                await _commandBus.Execute(command);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubTask([FromBody] SubTaskDTO subTaskDTO)
        {
            try
            {
                var command = new UpdateSubTaskCommand(
                    Guid.NewGuid(),
                    subTaskDTO.Id,
                    subTaskDTO.Name,
                    subTaskDTO.Description,
                    subTaskDTO.Status);

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
        public async Task<IActionResult> DeleteSubTask(int id)
        {
            try
            {
                var command = new DeleteSubTaskCommand(Guid.NewGuid(), id);

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
