using TaskManager.Core.Messaging;
using TaskManager.DomainModel.Common;

namespace TaskManager.Core.Commands.SubTaskCommands
{
    public interface ISubTaskCommand : ICommand
    {
        public string Name { get; }
        public string Description { get; }
        public Status Status { get; }
    }
}
