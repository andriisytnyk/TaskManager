﻿using System;

namespace TaskManager.Core.Exceptions
{
    public class TaskManagerException : Exception
    {
        public TaskManagerException(string message) : base(message)
        {

        }

        public TaskManagerException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
