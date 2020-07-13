using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.CommandHandlers.GlobalTaskCommandHandlers;
using TaskManager.Infrastructure.PipelineBuilder;

namespace TaskManager.Infrastructure.ExtensionMethods
{
    public static class TaskManagerPipelineBuilderExtensions
    {
        public static ITaskManagerPipelineBuilder UseGlobalTaskApi(this ITaskManagerPipelineBuilder builder)
        {
            builder.RegisterCommandHandler(sp => sp.GetService<CreateGlobalTaskCommandHandler>());
            return builder;
        }
    }
}
