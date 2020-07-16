using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;
using TaskManager.Infrastructure.Contexts.TaskManager;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskManagerRepository<T> : IRepository<T> where T : Entity
    {
        protected readonly TaskManagerContext taskManagerContext;

        public TaskManagerRepository(TaskManagerContext tmContext)
        {
            taskManagerContext = tmContext ?? throw new ArgumentNullException(nameof(tmContext));
        }

        public void Add(T entity)
        {
            taskManagerContext.Set<T>().Add(entity);

            taskManagerContext.SaveChanges();
        }

        //public void AddOrUpdate(T entity)
        //{
        //    if (entity.IsNew)
        //        Add(entity);
        //    else
        //        Update(entity);

        //    taskManagerContext.SaveChanges();
        //}

        public void Delete(T entity)
        {
            taskManagerContext.Entry(entity).State = EntityState.Deleted;

            taskManagerContext.SaveChanges();
        }

        public async Task<T> GetById(int id)
        {
            return await taskManagerContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetList(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public void Update(T entity)
        {
            taskManagerContext.Entry(entity).State = EntityState.Modified;

            taskManagerContext.SaveChanges();
        }

        //public async Task<int> CountAll()
        //{
        //    return await taskManagerContext.Set<T>().CountAsync();
        //}

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(taskManagerContext.Set<T>().AsQueryable(), specification);
        }
    }
}
