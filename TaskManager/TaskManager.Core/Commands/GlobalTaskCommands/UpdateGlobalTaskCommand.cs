using System;
using System.Collections.Generic;
using TaskManager.DomainModel.Common;

namespace TaskManager.Core.Commands.GlobalTaskCommands
{
    public class UpdateGlobalTaskCommand : IGlobalTaskCommand
    {
        public Guid Id { get; }
        public int GlobalTaskId { get; set; }
        public string Name { get; }
        public string Description { get; }
        public Status Status { get; }
        public DateTime FinishDate { get; }
        public IEnumerable<SubTaskApplicationModel> SubTasks { get; }

        public UpdateGlobalTaskCommand(
            Guid id,
            int globalTaskId,
            string name,
            string desc,
            Status status,
            DateTime finishDate,
            IEnumerable<SubTaskApplicationModel> subTasks)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot have '{Guid.Empty.ToString()}' value.");
            if (globalTaskId == default(int))
                throw new ArgumentException($"Incorrect format of field 'GlobalTaskId'. 'GlobalTaskId' cannot be 0.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Incorrect format of field 'Name'. 'Name' cannot be null or empty.");
            if (desc == null)
                throw new ArgumentException($"Incorrect format of field 'Description'. 'Description' cannot be null.");
            if (status == Status.Undefined)
                throw new ArgumentException($"Incorrect format of field 'Status'. 'Status' cannot be undefined.");
            if (finishDate == default)
                throw new ArgumentException($"Incorrect format of field 'FinishDate'. 'FinishDate' cannot be null.");
            if (subTasks == null)
                throw new ArgumentException($"Incorrect format of field 'SubTasks'. 'SubTasks' cannot be null.");

            Id = id;
            GlobalTaskId = globalTaskId;
            Name = name;
            Description = desc;
            Status = status;
            FinishDate = finishDate;
            SubTasks = subTasks;
        }
    }
}
