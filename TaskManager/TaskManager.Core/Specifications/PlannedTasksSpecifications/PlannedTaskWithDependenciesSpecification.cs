using BusinessRuleEngine.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.Specifications.PlannedTasksSpecifications
{
    public class PlannedTaskWithDependenciesSpecification : BaseSpecification<PlannedTask>
    {
        public PlannedTaskWithDependenciesSpecification() : base(pt => pt.Id > 0)
        {
            AddInclude("SubTasks");
        }
    }
}
