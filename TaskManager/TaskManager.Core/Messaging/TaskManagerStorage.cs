using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Exceptions;

namespace TaskManager.Core.Messaging
{
    public class TaskManagerStorage : IStorage
    {
        private readonly Dictionary<Type, object> _dataTypeToValue = new Dictionary<Type, object>();

        public Task<T> LoadData<T>()
        {
            if (!_dataTypeToValue.TryGetValue(typeof(T), out var value))
            {
                throw new TaskManagerException($"No save data for {typeof(T)}.");
            }

            return Task.FromResult((T)value);
        }

        public Task SaveData<T>(T data)
        {
            _dataTypeToValue[typeof(T)] = data;

            return Task.CompletedTask;
        }
    }
}
