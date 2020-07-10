using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Messaging
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task Handle(T @event);
    }
}
