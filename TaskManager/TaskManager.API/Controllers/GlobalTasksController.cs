using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.Core.Commands.GlobalTaskCommands;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.Core.Specifications.GlobalTasksSpecifications;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GlobalTasksController : ControllerBase
    {
        private readonly ICommandBus _commandBus;

        public IRepository<GlobalTask> GlobalTaskRepository { get; set; }

        public GlobalTasksController(IRepository<GlobalTask> globalTaskRepository, ICommandBus commandBus)
        {
            GlobalTaskRepository = globalTaskRepository;
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGlobalTasks()
        {
            try
            {
                var globalTaskWithDependenciesSpecification = new GlobalTaskWithDependenciesSpecification();

                return Ok(await GlobalTaskRepository.GetList(globalTaskWithDependenciesSpecification));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGlobalTask(int id)
        {
            try
            {
                var globalTaskByIdSpecification = new GlobalTaskByIdSpecification(id);

                return Ok((await GlobalTaskRepository.GetList(globalTaskByIdSpecification)).SingleOrDefault());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGlobalTask([FromBody] GlobalTaskDTO globalTaskDTO)
        {
            try
            {
                var command = new CreateGlobalTaskCommand(
                    Guid.NewGuid(),
                    globalTaskDTO.Name,
                    globalTaskDTO.Description,
                    globalTaskDTO.Status,
                    globalTaskDTO.FinishDate,
                    globalTaskDTO.SubTasks?.Select(st => new Core.Commands.SubTaskApplicationModel(
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
        public async Task<IActionResult> UpdateGlobalTask([FromBody] GlobalTaskDTO globalTaskDTO)
        {
            try
            {
                var command = new UpdateGlobalTaskCommand(
                    Guid.NewGuid(),
                    globalTaskDTO.Id,
                    globalTaskDTO.Name,
                    globalTaskDTO.Description,
                    globalTaskDTO.Status,
                    globalTaskDTO.FinishDate,
                    globalTaskDTO.SubTasks?.Select(st => new Core.Commands.SubTaskApplicationModel(
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
        public async Task<IActionResult> DeleteGlobalTask(int id)
        {
            try
            {
                var command = new DeleteGlobalTaskCommand(Guid.NewGuid(), id);

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
