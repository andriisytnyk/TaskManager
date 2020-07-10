using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Messaging
{
    public interface IEventBus
    {
        Task Publish<T>(T @event) where T : IEvent;

        void SubscribeEventHandler<T>(IEventHandler<T> @event) where T : IEvent;
    }
}
