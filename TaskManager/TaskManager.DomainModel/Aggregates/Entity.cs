namespace TaskManager.DomainModel.Aggregates
{
    public abstract class Entity
    {
        public int Id { get; set; }

        public bool IsNew => Id == default(int);
    }
}
