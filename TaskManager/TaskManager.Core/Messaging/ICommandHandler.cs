using System.Threading.Tasks;

namespace TaskManager.Core.Messaging
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task Handle(T command);
    }
}