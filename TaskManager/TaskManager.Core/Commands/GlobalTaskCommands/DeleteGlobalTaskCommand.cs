using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Messaging;

namespace TaskManager.Core.Commands.GlobalTaskCommands
{
    public class DeleteGlobalTaskCommand : ICommand
    {
        public Guid Id { get; }

        public DeleteGlobalTaskCommand(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot have '{Guid.Empty.ToString()}' value.");

            Id = id;
        }
    }
}
