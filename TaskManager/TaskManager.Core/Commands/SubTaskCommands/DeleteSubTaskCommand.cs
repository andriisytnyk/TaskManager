using System;
using TaskManager.Core.Messaging;

namespace TaskManager.Core.Commands.SubTaskCommands
{
    public class DeleteSubTaskCommand : ICommand
    {
        public Guid Id { get; }
        public int SubTaskId { get; set; }

        public DeleteSubTaskCommand(Guid id, int subTaskId)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot have '{Guid.Empty.ToString()}' value.");
            if (subTaskId == default(int))
                throw new ArgumentException($"Incorrect format of field 'SubTaskId'. 'SubTaskId' cannot be 0.");

            Id = id;
            SubTaskId = subTaskId;
        }
    }
}
