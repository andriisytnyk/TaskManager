using BusinessRuleEngine.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.Specifications.SubTasksSpecifications
{
    public class SubTasksAllSpecification : BaseSpecification<SubTask>
    {
        public SubTasksAllSpecification() : base(st => st.Id > 0)
        {

        }
    }
}
