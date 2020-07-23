using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using TaskManager.Logging.LoggingClasses;

namespace TaskManager.Logging
{
    public class TaskManagerLogger : ILogger, IDisposable
    {
        private static StreamWriter _streamWriter;

        public TaskManagerLogger()
        {
            if (_streamWriter != null)
                _streamWriter.Close();
            _streamWriter = new StreamWriter($"{DateTime.Now.Date.ToString("yyyy-MM-dd")}.log", true);
        }

        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            try
            {
                switch(state)
                {
                    case EnterLogMethod entry:
                        LogEntry(logLevel, entry.MethodName, entry.Message, entry.TransactionId, entry.Arguments.ToArray());
                        break;
                    case ExitLogMethod entry:
                        LogExit(logLevel, entry.MethodName, entry.Message, entry.TransactionId, entry.Arguments.ToArray());
                        break;
                    case ExitFailedLogMethod entry:
                        LogException(logLevel, entry.MethodName, entry.Message, entry.Exception, entry.TransactionId, entry.Arguments.ToArray());
                        break;
                    default:
                        LogState(logLevel, eventId, state, exception, formatter);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogException(
                    logLevel,
                    MethodBase.GetCurrentMethod().ReflectedType.FullName,
                    $@"Method {MethodBase.GetCurrentMethod().ReflectedType.FullName} failed.",
                    ex,
                    Guid.Empty);

                throw;
            }
        }

        private void LogEntry(LogLevel logLevel, string methodName, string message, Guid id, params object[] arguments)
        {
            _streamWriter.WriteLine($"{DateTime.Now}: {logLevel}; Method name: {methodName}; Message: {message}; Request/transaction id: {id}; Arguments: {string.Join(", ", arguments)}");
        }

        private void LogExit(LogLevel logLevel, string methodName, string message, Guid id, params object[] arguments)
        {
            _streamWriter.WriteLine($"{DateTime.Now}: {logLevel}; Method name: {methodName}; Message: {message}; Request/transaction id: {id}; Arguments: {string.Join(", ", arguments)}");
        }

        private void LogException(LogLevel logLevel, string methodName, string message, Exception exception, Guid id, params object[] arguments)
        {
            _streamWriter.WriteLine($"{DateTime.Now}: {logLevel}; Method name: {methodName}; Message: {message}; Request/transaction id: {id}; Exception: {exception}; Stack trace: {exception.StackTrace}; Arguments: {string.Join(", ", arguments)}");
        }

        private void LogState<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            _streamWriter.WriteLine($"{DateTime.Now}: {logLevel}; State: {state}");
        }

        #region Disposing

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _streamWriter.Close();
                _streamWriter.Dispose();
            }

            // clear unmanaged resources

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        ~TaskManagerLogger()
        {
            Dispose(false);
        }

        #endregion
    }
}
