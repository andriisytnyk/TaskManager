using System.Threading.Tasks;

namespace TaskManager.Core.Messaging
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task Handle(T @event);
    }
}
