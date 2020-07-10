using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Messaging;

namespace TaskManager.Core.Commands.SubTaskCommands
{
    public class DeleteSubTaskCommand : ICommand
    {
        public Guid Id { get; }

        public DeleteSubTaskCommand(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot have '{Guid.Empty.ToString()}' value.");

            Id = id;
        }
    }
}
