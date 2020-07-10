using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Messaging;

namespace TaskManager.Core.Commands.PlannedTaskCommands
{
    public class DeletePlannedTaskCommand : ICommand
    {
        public Guid Id { get; }

        public DeletePlannedTaskCommand(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot have '{Guid.Empty.ToString()}' value.");

            Id = id;
        }
    }
}
