using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Messaging
{
    public interface ICommandBus
    {
        Task Execute<T>(T command) where T : ICommand;

        void RegisterCommandHandler<T>(ICommandHandler<T> handler) where T : ICommand;
    }
}
