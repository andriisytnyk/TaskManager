﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }

        public void AddOrUpdate(T entity)
        {
            if (entity.IsNew)
                Add(entity);
            else
                Update(entity);
        }

        public async Task<int> CountAll()
        {
            return await taskManagerContext.Set<T>().CountAsync();
        }

        public void Delete(T entity)
        {
            taskManagerContext.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await taskManagerContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await taskManagerContext.Set<T>().FindAsync(id);
        }

        public Task<IReadOnlyList<T>> GetList(ISpecification<T> specification)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            taskManagerContext.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(taskManagerContext.Set<T>().AsQueryable(), specification);
        }
    }
}
