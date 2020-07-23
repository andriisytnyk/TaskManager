using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Logging
{
    [ProviderAlias("TMLogger")]
    public class TaskManagerLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, TaskManagerLogger> _categoryNameToLoggerMap = 
            new ConcurrentDictionary<string, TaskManagerLogger>();

        public ILogger CreateLogger(string categoryName)
        {
            return _categoryNameToLoggerMap.GetOrAdd(categoryName, category => new TaskManagerLogger());
        }

        #region Disposing

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                foreach (var logger in _categoryNameToLoggerMap.Values)
                {
                    logger.Dispose();
                }
            }

            _categoryNameToLoggerMap.Clear();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        ~TaskManagerLoggerProvider()
        {
            Dispose(false);
        }

        #endregion
    }
}
