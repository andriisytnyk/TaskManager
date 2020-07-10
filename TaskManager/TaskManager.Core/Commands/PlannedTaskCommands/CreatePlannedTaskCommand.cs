﻿using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Common;

namespace TaskManager.Core.Commands.PlannedTaskCommands
{
    public class CreatePlannedTaskCommand : IPlannedTaskCommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Status Status { get; }
        public DateTime StartDateTime { get; }
        public DateTime FinishDateTime { get; }
        public TimeSpan Estimation { get; }
        public bool Requirement { get; }
        public Frequency Frequency { get; }
        public IEnumerable<SubTaskApplicationModel> SubTasks { get; }

        public CreatePlannedTaskCommand(
            Guid id,
            string name,
            string desc,
            Status status,
            DateTime startDt,
            DateTime finishDt,
            TimeSpan estimation,
            bool requirement,
            Frequency frequency,
            IEnumerable<SubTaskApplicationModel> subTasks)
        {
            if (id == Guid.Empty)
                throw new ArgumentException($"{nameof(id)} cannot have '{Guid.Empty.ToString()}' value.");
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"Incorrect format of field 'Name'. 'Name' cannot be null or empty.");
            if (desc == null)
                throw new ArgumentException($"Incorrect format of field 'Description'. 'Description' cannot be null.");
            if (status == Status.Undefined)
                throw new ArgumentException($"Incorrect format of field 'Status'. 'Status' cannot be undefined.");
            if (startDt == default)
                throw new ArgumentException($"Incorrect format of field 'StartDateTime'. 'StartDateTime' cannot be null.");
            if (finishDt == default)
                throw new ArgumentException($"Incorrect format of field 'FinishDateTime'. 'FinishDateTime' cannot be null.");
            if (estimation == default)
                throw new ArgumentException($"Incorrect format of field 'Estimation'. 'Estimation' cannot be null.");
            if (subTasks == null)
                throw new ArgumentException($"Incorrect format of field 'SubTasks'. 'SubTasks' cannot be null.");

            Id = id;
            Name = name;
            Description = desc;
            Status = status;
            StartDateTime = startDt;
            FinishDateTime = finishDt;
            Estimation = estimation;
            Requirement = requirement;
            Frequency = frequency;
            SubTasks = subTasks;
        }
    }
}
