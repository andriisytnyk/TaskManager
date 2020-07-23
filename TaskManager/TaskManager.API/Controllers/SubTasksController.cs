using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.API.DTOs;
using TaskManager.Core.Commands.SubTaskCommands;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.Core.Specifications.SubTasksSpecifications;
using TaskManager.DomainModel.Aggregates;
using TaskManager.Logging;
using TaskManager.Logging.LoggingClasses;

namespace TaskManager.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SubTasksController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ILogger<SubTasksController> _logger;

        public IRepository<SubTask> SubTaskRepository { get; set; }

        public SubTasksController(IRepository<SubTask> subTaskRepository, ICommandBus commandBus, ILogger<SubTasksController> logger)
        {
            SubTaskRepository = subTaskRepository;
            _commandBus = commandBus;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSubTasks()
        {
            var requestId = Guid.NewGuid();

            _logger.LogMessage(
                LogLevel.Information,
                new EnterLogMethod(
                    MethodBase.GetCurrentMethod().ReflectedType.FullName,
                    $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} started.",
                    requestId));

            try
            {
                var subTasksAllSpecification = new SubTasksAllSpecification();

                var subTasks = await SubTaskRepository.GetList(subTasksAllSpecification);

                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} finished.",
                        requestId));

                return Ok(subTasks);
            }
            catch (Exception ex)
            {
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.Name,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                        ex));

                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetSubTask(int id)
        {
            var requestId = Guid.NewGuid();

            _logger.LogMessage(
                LogLevel.Information,
                new EnterLogMethod(
                    MethodBase.GetCurrentMethod().ReflectedType.FullName,
                    $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} started.",
                    requestId));

            try
            {
                var subTaskByIdSpecification = new SubTaskByIdSpecification(id);

                var subTask = (await SubTaskRepository.GetList(subTaskByIdSpecification)).SingleOrDefault();

                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} finished.",
                        requestId));

                return Ok(subTask);
            }
            catch (Exception ex)
            {
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.Name,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                        ex));

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
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.Name,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                        ex));

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
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.Name,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                        ex));

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
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.Name,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                        ex));

                return BadRequest(ex);
            }
        }
    }
}
