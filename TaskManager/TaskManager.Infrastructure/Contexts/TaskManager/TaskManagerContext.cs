using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Aggregates;

namespace TaskManager.Infrastructure.Contexts.TaskManager
{
    public partial class TaskManagerContext : DbContext
    {
        private const string TaskPrivatePropertyName = "SubTasks";
        private const string ParentTaskName = "ParentTask";

        public TaskManagerContext()
        {

        }

        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
        {

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
