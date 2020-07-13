using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Messaging;

namespace TaskManager.Infrastructure.PipelineBuilder
{
    public interface ITaskManagerPipelineBuilder
    {
        void RegisterCommandHandler<T>(Func<IServiceProvider, ICommandHandler<T>> func) where T : ICommand;
    }
}
