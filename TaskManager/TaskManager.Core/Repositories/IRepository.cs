using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Core.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetById(int id);

        Task<IReadOnlyList<T>> GetList(ISpecification<T> specification);

        void Add(T entity);

        void Update(T entity);

        //void AddOrUpdate(T entity);

        void Delete(T entity);

        //Task<int> CountAll();
    }
}
