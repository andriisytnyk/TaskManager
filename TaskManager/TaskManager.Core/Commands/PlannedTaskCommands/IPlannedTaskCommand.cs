using System;
using System.Collections.Generic;
using TaskManager.Core.Messaging;
using TaskManager.DomainModel.Common;

namespace TaskManager.Core.Commands.PlannedTaskCommands
{
    public interface IPlannedTaskCommand : ICommand
    {
        public string Name { get; }
        public string Description { get; }
        public Status Status { get; }
        public DateTime StartDateTime { get; }
        public DateTime FinishDateTime { get; }
        public TimeSpan Estimation { get; }
        public bool Requirement { get; }
        public Frequency Frequency { get; }
        public IEnumerable<SubTaskApplicationModel> SubTasks { get; }
    }
}
