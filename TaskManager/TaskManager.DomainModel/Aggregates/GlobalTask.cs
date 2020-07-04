using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.DomainModel.Aggregates
{
    public class GlobalTask : Task
    {
        public DateTime FinishDate { get; set; }
        public List<SubTask> SubTasks { get; set; }
    }
}
