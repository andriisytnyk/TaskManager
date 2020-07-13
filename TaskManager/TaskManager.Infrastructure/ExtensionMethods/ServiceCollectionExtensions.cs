using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.CommandHandlers.GlobalTaskCommandHandlers;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;
using TaskManager.Infrastructure.Contexts.TaskManager;
using TaskManager.Infrastructure.PipelineBuilder;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Infrastructure.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection UseCommandHandlers(this IServiceCollection services)
        {
            UseGlobalTaskCommandHandlers(services);
            services.AddScoped<ITaskManagerPipelineBuilder, TaskManagerPipelineBuilder>();
            return services;
        }

        public static IServiceCollection UseEfCore(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddDbContext<TaskManagerContext>(optionsAction);

            services.AddScoped<IRepository<GlobalTask>, GlobalTaskRepository>();
            services.AddScoped<IRepository<PlannedTask>, PlannedTaskRepository>();
            services.AddScoped<IRepository<SubTask>, SubTaskRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetService<TaskManagerContext>());

            return services;
        }

        private static void UseGlobalTaskCommandHandlers(IServiceCollection services)
        {
            services.AddScoped<CreateGlobalTaskCommandHandler>();
        }
    }
}
