using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Common;

namespace TaskManager.DomainModel.Aggregates
{
    public class PlannedTask : Task
    {
        public DateTime StartDateTime { get; set; }
        public DateTime FinishDateTime { get; set; }
        public TimeSpan Estimation { get; set; }
        public bool Requirement { get; set; }
        public Frequency Frequency { get; set; }
        public IEnumerable<SubTask> SubTasks { get; set; }
    }
}
