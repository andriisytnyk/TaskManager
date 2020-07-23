using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskManager.API.DTOs;
using TaskManager.Core.Commands.GlobalTaskCommands;
using TaskManager.Core.Messaging;
using TaskManager.Core.Repositories;
using TaskManager.Core.Specifications.GlobalTasksSpecifications;
using TaskManager.DomainModel.Aggregates;
using TaskManager.Logging;
using TaskManager.Logging.LoggingClasses;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class GlobalTasksController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly ILogger<GlobalTasksController> _logger;

        public IRepository<GlobalTask> GlobalTaskRepository { get; set; }

        public GlobalTasksController(IRepository<GlobalTask> globalTaskRepository, ICommandBus commandBus, ILogger<GlobalTasksController> logger)
        {
            GlobalTaskRepository = globalTaskRepository;
            _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGlobalTasks()
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
                var globalTaskWithDependenciesSpecification = new GlobalTaskWithDependenciesSpecification();

                var globalTasks = await GlobalTaskRepository.GetList(globalTaskWithDependenciesSpecification);

                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} finished.",
                        requestId));

                return Ok(globalTasks);
            }
            catch (Exception ex)
            {
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                        ex));

                return BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGlobalTask(int id)
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
                var globalTaskByIdSpecification = new GlobalTaskByIdSpecification(id);

                var globalTask = (await GlobalTaskRepository.GetList(globalTaskByIdSpecification)).SingleOrDefault();

                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} finished.",
                        requestId));

                return Ok(globalTask);
            }
            catch (Exception ex)
            {
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                        ex));

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
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                        ex));

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
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                        ex));

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
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,
                        $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                        ex));

                return BadRequest(ex);
            }
        }
    }
}
