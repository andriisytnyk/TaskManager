using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using TaskManager.Infrastructure.ExtensionMethods;
using TaskManager.Infrastructure.PipelineBuilder;

namespace TaskManager.API.Middleware
{
    public class TaskManagerPipelineMiddleware
    {
        private readonly RequestDelegate _next;

        public TaskManagerPipelineMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context, ITaskManagerPipelineBuilder builder)
        {
            builder.UseGlobalTaskApi();
            await _next.Invoke(context).ConfigureAwait(false);
        }
    }
}
