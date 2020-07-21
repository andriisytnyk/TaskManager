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
