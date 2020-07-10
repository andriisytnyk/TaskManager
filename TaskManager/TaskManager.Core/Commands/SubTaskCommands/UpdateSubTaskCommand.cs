using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Common;

namespace TaskManager.Core.Commands.SubTaskCommands
{
    public class UpdateSubTaskCommand : ISubTaskCommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Status Status { get; }

        public UpdateSubTaskCommand(
            Guid id,
            string name,
            string desc,
            Status status)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot have '{Guid.Empty.ToString()}' value.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Incorrect format of field 'Name'. 'Name' cannot be null or empty.");
            if (desc == null)
                throw new ArgumentException($"Incorrect format of field 'Description'. 'Description' cannot be null.");
            if (status == Status.Undefined)
                throw new ArgumentException($"Incorrect format of field 'Status'. 'Status' cannot be undefined.");

            Id = id;
            Name = name;
            Description = desc;
            Status = status;
        }
    }
}
