using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.Specifications.GlobalTasksSpecifications
{
    public class GlobalTaskWithDependenciesSpecification : BaseSpecification<GlobalTask>
    {
        public GlobalTaskWithDependenciesSpecification() : base(gt => gt.Id > 0)
        {
            AddInclude("SubTasks");
        }
    }
}
