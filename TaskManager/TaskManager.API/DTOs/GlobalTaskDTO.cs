using System;
using System.Collections.Generic;

namespace TaskManager.API.DTOs
{
    public class GlobalTaskDTO : TaskDTO
    {
        public DateTime FinishDate { get; set; }
        public List<SubTaskDTO> SubTasks { get; set; }
    }
}
