using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Logging.LoggingClasses
{
    public class LogMethod
    {
        public IReadOnlyCollection<object> Arguments { get; }
        public string MethodName { get; }
        public string Message { get; set; }
        public Guid TransactionId { get; }

        public LogMethod(string methodName, string message, Guid transactionId = default, params object[] arguments)
        {
            MethodName = methodName ?? throw new ArgumentNullException(nameof(methodName));
            Message = message;
            TransactionId = transactionId;
            Arguments = arguments;
        }
    }
}
