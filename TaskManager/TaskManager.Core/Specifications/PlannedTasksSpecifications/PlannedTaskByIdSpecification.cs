using BusinessRuleEngine.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.Specifications.PlannedTasksSpecifications
{
    public class PlannedTaskByIdSpecification : BaseSpecification<PlannedTask>
    {
        public PlannedTaskByIdSpecification(int id) : base(pt => pt.Id == id)
        {
            AddInclude("SubTasks");
        }
    }
}
