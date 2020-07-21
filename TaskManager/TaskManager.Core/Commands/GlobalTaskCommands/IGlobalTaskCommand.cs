using System;
using System.Collections.Generic;
using TaskManager.Core.Messaging;
using TaskManager.DomainModel.Common;

namespace TaskManager.Core.Commands.GlobalTaskCommands
{
    public interface IGlobalTaskCommand : ICommand
    {
        public string Name { get; }
        public string Description { get; }
        public Status Status { get; }
        public DateTime FinishDate { get; }
        public IEnumerable<SubTaskApplicationModel> SubTasks { get; }
    }
}
