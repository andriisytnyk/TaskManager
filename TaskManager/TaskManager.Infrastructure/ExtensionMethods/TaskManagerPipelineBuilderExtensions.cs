using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.CommandHandlers.GlobalTaskCommandHandlers;
using TaskManager.Core.CommandHandlers.PlannedTaskCommandHandlers;
using TaskManager.Core.CommandHandlers.SubTaskCommandHandlers;
using TaskManager.Infrastructure.PipelineBuilder;

namespace TaskManager.Infrastructure.ExtensionMethods
{
    public static class TaskManagerPipelineBuilderExtensions
    {
        public static ITaskManagerPipelineBuilder UseGlobalTaskApi(this ITaskManagerPipelineBuilder builder)
        {
            builder.RegisterCommandHandler(sp => sp.GetService<CreateGlobalTaskCommandHandler>());
            builder.RegisterCommandHandler(sp => sp.GetService<UpdateGlobalTaskCommandHandler>());
            builder.RegisterCommandHandler(sp => sp.GetService<DeleteGlobalTaskCommandHandler>());

            return builder;
        }

        public static ITaskManagerPipelineBuilder UsePlannedTaskApi(this ITaskManagerPipelineBuilder builder)
        {
            builder.RegisterCommandHandler(sp => sp.GetService<CreatePlannedTaskCommandHandler>());
            builder.RegisterCommandHandler(sp => sp.GetService<UpdatePlannedTaskCommandHandler>());
            builder.RegisterCommandHandler(sp => sp.GetService<DeletePlannedTaskCommandHandler>());

            return builder;
        }

        public static ITaskManagerPipelineBuilder UseSubTaskApi(this ITaskManagerPipelineBuilder builder)
        {
            builder.RegisterCommandHandler(sp => sp.GetService<CreateSubTaskCommandHandler>());
            builder.RegisterCommandHandler(sp => sp.GetService<UpdateSubTaskCommandHandler>());
            builder.RegisterCommandHandler(sp => sp.GetService<DeleteSubTaskCommandHandler>());

            return builder;
        }
    }
}
