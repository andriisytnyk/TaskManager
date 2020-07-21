using System;
using TaskManager.Core.Messaging;

namespace TaskManager.Core.Commands.GlobalTaskCommands
{
    public class DeleteGlobalTaskCommand : ICommand
    {
        public Guid Id { get; }
        public int GlobalTaskId { get; set; }

        public DeleteGlobalTaskCommand(Guid id, int globalTaskId)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot have '{Guid.Empty.ToString()}' value.");
            if (globalTaskId == default(int))
                throw new ArgumentException($"Incorrect format of field 'GlobalTaskId'. 'GlobalTaskId' cannot be 0.");

            Id = id;
            GlobalTaskId = globalTaskId;
        }
    }
}
