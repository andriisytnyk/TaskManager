using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Logging.LoggingClasses;

namespace TaskManager.Logging
{
    public static class LoggerExtensions
    {
        public static ILoggingBuilder AddTMLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, TaskManagerLoggerProvider>();

            return builder;
        }

        public static void LogMessage<T>(this ILogger logger, LogLevel logLevel, T logMethod) where T : LogMethod
        {
            if (logger == null)
                throw new ArgumentNullException(nameof(logger));

            logger.Log(logLevel, default, logMethod, null, null);
        }
    }
}
