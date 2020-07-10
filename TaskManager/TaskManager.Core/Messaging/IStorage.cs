using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Messaging
{
    public interface IStorage
    {
        Task<T> LoadData<T>();

        Task SaveData<T>(T data);
    }
}
