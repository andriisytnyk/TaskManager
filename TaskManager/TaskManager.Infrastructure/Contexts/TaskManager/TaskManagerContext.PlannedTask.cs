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
        private const string PlannedTaskTableName = "PlannedTasks";

        public DbSet<GlobalTask> PlannedTasks { get; set; }

        public void ConfigurePlannedTask(EntityTypeBuilder<PlannedTask> builder)
        {
            builder.ToTable(PlannedTaskTableName);
            builder.HasKey(g => g.Id).IsClustered();
            builder.Property(g => g.Name).HasMaxLength(30).HasDefaultValue("New task").IsRequired();
            builder.Property(g => g.Description).HasMaxLength(100);
            builder.Property(g => g.Status).HasDefaultValue(Status.ToDo).IsRequired();
            builder.Property(g => g.StartDateTime).IsRequired();
            builder.Property(g => g.FinishDateTime).IsRequired();
            builder.Property(g => g.Estimation).IsRequired();
            builder.Property(g => g.Requirement).HasDefaultValue(true).IsRequired();
            builder.Property(g => g.Frequency).HasDefaultValue(Frequency.NonRepeating).IsRequired();
            builder.HasMany<SubTask>(TaskPrivatePropertyName)
                .WithOne()
                .HasForeignKey(ParentTaskName)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
