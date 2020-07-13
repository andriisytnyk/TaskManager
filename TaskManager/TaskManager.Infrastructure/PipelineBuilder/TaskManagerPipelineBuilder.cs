using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Messaging;

namespace TaskManager.Infrastructure.PipelineBuilder
{
    public class TaskManagerPipelineBuilder : ITaskManagerPipelineBuilder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICommandBus _commandBus;

        public TaskManagerPipelineBuilder(IServiceProvider serviceProvider, ICommandBus commandBus)
        {
            _serviceProvider = serviceProvider;
            _commandBus = commandBus;
        }

        public void RegisterCommandHandler<T>(Func<IServiceProvider, ICommandHandler<T>> func) where T : ICommand
        {
            ICommandHandler<T> handler = func(_serviceProvider);
            _commandBus.RegisterCommandHandler(handler);
        }
    }
}
