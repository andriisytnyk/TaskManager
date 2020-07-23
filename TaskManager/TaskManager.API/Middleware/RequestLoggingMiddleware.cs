using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using System;
using System.IO;
using System.Threading.Tasks;
using TaskManager.Logging;
using TaskManager.Logging.LoggingClasses;

namespace TaskManager.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestId = Guid.NewGuid();

            _logger.LogMessage(
                LogLevel.Information,
                new EnterLogMethod(
                    context.Request.Method,
                    $@"Method {context.Request.Method} started.",
                    requestId,
                    context));

            try
            {
                await _next.Invoke(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.LogMessage(
                    LogLevel.Information,
                    new ExitFailedLogMethod(
                        context.Request.Method,
                        $@"Method {context.Request.Method} failed.",
                        ex,
                        requestId,
                        context));

                throw;
            }

            _logger.LogMessage(
                LogLevel.Information,
                new ExitLogMethod(
                    context.Request.Method,
                    $@"Method {context.Request.Method} finished.",
                    requestId,
                    context));
        }
    }
}
