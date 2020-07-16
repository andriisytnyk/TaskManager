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
