using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Common;

namespace TaskManager.API.DTOs
{
    public class PlannedTaskDTO : TaskDTO
    {
        public DateTime StartDateTime { get; set; }
        public DateTime FinishDateTime { get; set; }
        public TimeSpan Estimation { get; set; }
        public bool Requirement { get; set; }
        public Frequency Frequency { get; set; }
        public List<SubTaskDTO> SubTasks { get; set; }
    }
}
