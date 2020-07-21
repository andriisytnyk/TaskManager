using System;
using TaskManager.DomainModel.Common;

namespace TaskManager.Core.Commands.SubTaskCommands
{
    public class UpdateSubTaskCommand : ISubTaskCommand
    {
        public Guid Id { get; }
        public int SubTaskId { get; set; }
        public string Name { get; }
        public string Description { get; }
        public Status Status { get; }

        public UpdateSubTaskCommand(
            Guid id,
            int subTaskId,
            string name,
            string desc,
            Status status)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot have '{Guid.Empty.ToString()}' value.");
            if (subTaskId == default(int))
                throw new ArgumentException($"Incorrect format of field 'SubTaskId'. 'SubTaskId' cannot be 0.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Incorrect format of field 'Name'. 'Name' cannot be null or empty.");
            if (desc == null)
                throw new ArgumentException($"Incorrect format of field 'Description'. 'Description' cannot be null.");
            if (status == Status.Undefined)
                throw new ArgumentException($"Incorrect format of field 'Status'. 'Status' cannot be undefined.");

            Id = id;
            SubTaskId = subTaskId;
            Name = name;
            Description = desc;
            Status = status;
        }
    }
}
