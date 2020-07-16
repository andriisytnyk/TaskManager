using System;
using TaskManager.Core.Messaging;

namespace TaskManager.Core.Commands.PlannedTaskCommands
{
    public class DeletePlannedTaskCommand : ICommand
    {
        public Guid Id { get; }
        public int PlannedTaskId { get; set; }

        public DeletePlannedTaskCommand(Guid id, int plannedTaskId)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot have '{Guid.Empty.ToString()}' value.");
            if (plannedTaskId == default(int))
                throw new ArgumentException($"Incorrect format of field 'PlannedTaskId'. 'PlannedTaskId' cannot be 0.");

            Id = id;
            PlannedTaskId = plannedTaskId;
        }
    }
}
