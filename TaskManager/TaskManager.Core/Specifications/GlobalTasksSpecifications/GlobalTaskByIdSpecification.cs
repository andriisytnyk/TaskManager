using BusinessRuleEngine.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.Specifications.GlobalTasksSpecifications
{
    public class GlobalTaskByIdSpecification : BaseSpecification<GlobalTask>
    {
        public GlobalTaskByIdSpecification(int id) : base(gt => gt.Id == id)
        {
            AddInclude("SubTasks");
        }
    }
}
