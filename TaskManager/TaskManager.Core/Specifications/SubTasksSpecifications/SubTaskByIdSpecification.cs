using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.Specifications.SubTasksSpecifications
{
    public class SubTaskByIdSpecification : BaseSpecification<SubTask>
    {
        public SubTaskByIdSpecification(int id) : base(st => st.Id == id)
        {

        }
    }
}
