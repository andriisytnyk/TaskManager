namespace TaskManager.DomainModel.Aggregates
{
    public abstract class DescriptiveEntity : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
