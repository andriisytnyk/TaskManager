using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Logging.LoggingClasses
{
    public class ExitFailedLogMethod : LogMethod
    {
        public Exception Exception { get; }

        public ExitFailedLogMethod(string methodName, string message, Exception exception, Guid transactionId = default, params object[] arguments) : base(methodName, message, transactionId, arguments)
        {
            Exception = exception;
        }
    }
}
