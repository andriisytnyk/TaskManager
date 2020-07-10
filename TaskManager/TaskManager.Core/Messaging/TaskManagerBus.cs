using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Exceptions;

namespace TaskManager.Core.Messaging
{
    public class TaskManagerBus : ICommandBus
    {
        private readonly Dictionary<Type, object> _commandTypeToCommandHandler = new Dictionary<Type, object>();
        private readonly ILogger<TaskManagerBus> _logger;

        public TaskManagerBus(ILogger<TaskManagerBus> logger)
        {
            _logger = logger;
        }

        public async Task Execute<T>(T command) where T : ICommand
        {
            if (!_commandTypeToCommandHandler.TryGetValue(typeof(T), out var handler))
                throw new TaskManagerException($"Unable to find handler for {typeof(T).Name}.");

            try
            {
                await (handler as ICommandHandler<T>).Handle(command);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RegisterCommandHandler<T>(ICommandHandler<T> handler) where T : ICommand
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            _commandTypeToCommandHandler.Add(typeof(T), handler);
        }
    }
}
