using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TaskManager.Core.Exceptions;
using TaskManager.Logging;
using TaskManager.Logging.LoggingClasses;

namespace TaskManager.Core.Messaging
{
    public class TaskManagerBus : ICommandBus
    {
        private readonly Dictionary<Type, object> _commandTypeToCommandHandler = new Dictionary<Type, object>();
        private readonly ILogger<TaskManagerBus> _logger;

        public TaskManagerBus(ILogger<TaskManagerBus> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Execute<T>(T command) where T : ICommand
        {
            _logger.LogMessage(
                LogLevel.Information, 
                new EnterLogMethod(
                    MethodBase.GetCurrentMethod().ReflectedType.FullName, 
                    $@"Method 'Execute<{command.GetType()}>' started.",
                    command.Id));

            if (!_commandTypeToCommandHandler.TryGetValue(typeof(T), out var handler))
            {
                var exception = new TaskManagerException($"Unable to find handler for {typeof(T).Name}.");

                _logger.LogMessage(
                    LogLevel.Information, 
                    new ExitFailedLogMethod(
                        MethodBase.GetCurrentMethod().ReflectedType.FullName,  
                        $@"Unable to find handler for {typeof(T).Name} in method 'Execute<{command.GetType()}>'.",
                        exception,
                        command.Id));

                throw exception;
            }

            try
            {
                await (handler as ICommandHandler<T>).Handle(command);
            }
            catch (Exception)
            {
                throw;
            }

            _logger.LogMessage(
                LogLevel.Information,
                new ExitLogMethod(
                    MethodBase.GetCurrentMethod().ReflectedType.FullName,  
                    $@"Method 'Execute<{command.GetType()}>' finished.",
                    command.Id));
        }

        public void RegisterCommandHandler<T>(ICommandHandler<T> handler) where T : ICommand
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            _commandTypeToCommandHandler.Add(typeof(T), handler);
        }
    }
}
