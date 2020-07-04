using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Common;

namespace TaskManager.DomainModel.Aggregates
{
    public abstract class Task : DescriptiveEntity
    {
        public Status Status { get; set; }
    }
}
