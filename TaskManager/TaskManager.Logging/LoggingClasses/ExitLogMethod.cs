﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Logging.LoggingClasses
{
    public class ExitLogMethod : LogMethod
    {
        public ExitLogMethod(string methodName, string message, Guid transactionId, params object[] arguments) : base(methodName, message, transactionId, arguments)
        {
            
        }
    }
}
