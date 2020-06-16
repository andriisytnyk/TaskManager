using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Aggregates;
using TaskManager.DomainModel.Common;

namespace TaskManager.Infrastructure.Contexts.TaskManager
{
    public partial class TaskManagerContext
    {
        private const string SubTaskTableName = "SubTasks";

        public DbSet<SubTask> SubTasks { get; set; }

        public void ConfigureSubTask(EntityTypeBuilder<SubTask> builder)
        {
            builder.ToTable(SubTaskTableName);
            builder.HasKey(t => t.Id).IsClustered();
            builder.Property(t => t.Name).HasMaxLength(30).HasDefaultValue("New task").IsRequired();
            builder.Property(t => t.Description).HasMaxLength(100);
            builder.Property(t => t.Status).HasDefaultValue(Status.ToDo).IsRequired();
        }
    }
}
