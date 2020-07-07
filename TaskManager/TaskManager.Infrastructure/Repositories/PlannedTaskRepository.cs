using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Aggregates;
using TaskManager.Infrastructure.Contexts.TaskManager;

namespace TaskManager.Infrastructure.Repositories
{
    public class PlannedTaskRepository : TaskManagerRepository<PlannedTask>
    {
        public PlannedTaskRepository(TaskManagerContext taskManagerContext) : base(taskManagerContext)
        {

        }
    }
}
