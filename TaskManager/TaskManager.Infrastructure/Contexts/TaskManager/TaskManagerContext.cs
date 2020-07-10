using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Core.Repositories;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Infrastructure.Contexts.TaskManager
{
    public partial class TaskManagerContext : DbContext, IUnitOfWork
    {
        private const string TaskPrivatePropertyName = "SubTasks";
        private const string ParentGlobalTaskName = "ParentGlobalTask";
        private const string ParentPlannedTaskName = "ParentPlannedTask";

        public TaskManagerContext()
        {

        }

        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
        {

        }

        public async Task<ITransaction> BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted, CancellationToken cancellationToken = default)
        {
            var transaction = await Database.BeginTransactionAsync(isolationLevel, cancellationToken);

            return new DbContextTransaction(transaction);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubTask>(ConfigureSubTask);
            modelBuilder.Entity<GlobalTask>(ConfigureGlobalTask);
            modelBuilder.Entity<PlannedTask>(ConfigurePlannedTask);
        }
    }
}
