using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.API.DTOs;
using TaskManager.Core.Commands.PlannedTaskCommands;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.Core.Specifications.PlannedTasksSpecifications;
using TaskManager.DomainModel.Aggregates;
using TaskManager.Logging;
using TaskManager.Logging.LoggingClasses;

namespace TaskManager.API.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlannedTasksController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ILogger<PlannedTasksController> _logger;

        public IRepository<PlannedTask> PlannedTaskRepository { get; set; }

        public PlannedTasksController(IRepository<PlannedTask> plannedTaskRepository, ICommandBus commandBus, ILogger<PlannedTasksController> logger)
        {
            PlannedTaskRepository = plannedTaskRepository;
            _commandBus = commandBus;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlannedTasks()
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
                var plannedTaskWithDependenciesSpecification = new PlannedTaskWithDependenciesSpecification();

                var plannedTasks = await PlannedTaskRepository.GetList(plannedTaskWithDependenciesSpecification);

                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} finished.",
                        requestId));

                return Ok(plannedTasks);
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
        public async Task<IActionResult> GetPlannedTask(int id)
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
                var plannedTaskByIdSpecification = new PlannedTaskByIdSpecification(id);

                var plannedTask = (await PlannedTaskRepository.GetList(plannedTaskByIdSpecification)).SingleOrDefault();

                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} finished.",
                        requestId));

                return Ok(plannedTask);
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
