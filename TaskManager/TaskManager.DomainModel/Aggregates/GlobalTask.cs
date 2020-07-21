using System;
using System.Collections.Generic;

namespace TaskManager.DomainModel.Aggregates
{
    public class GlobalTask : Task
    {
        public DateTime FinishDate { get; set; }
        public IEnumerable<SubTask> SubTasks { get; set; }
    }
}
