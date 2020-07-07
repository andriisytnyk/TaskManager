using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Common;

namespace TaskManager.API.DTOs
{
    public abstract class TaskDTO : DescriptiveEntityDTO
    {
        public Status Status { get; set; }
    }
}
