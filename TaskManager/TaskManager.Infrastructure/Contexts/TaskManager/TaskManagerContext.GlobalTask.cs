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
        private const string GlobalTaskTableName = "GlobalTasks";

        public DbSet<GlobalTask> GlobalTasks { get; set; }

        public void ConfigureGlobalTask(EntityTypeBuilder<GlobalTask> builder)
        {
            builder.ToTable(TaskTableName);
            builder.HasKey(g => g.Id).IsClustered();
            builder.Property(g => g.Name).HasMaxLength(30).HasDefaultValue("New task").IsRequired();
            builder.Property(g => g.Description).HasMaxLength(100);
            builder.Property(g => g.Status).HasDefaultValue(Status.ToDo).IsRequired();
            builder.Property(g => g.FinishDate).IsRequired();
            builder.HasMany<Task>(TaskPrivatePropertyName)
                .WithOne()
                .HasForeignKey(ParentTaskName)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
