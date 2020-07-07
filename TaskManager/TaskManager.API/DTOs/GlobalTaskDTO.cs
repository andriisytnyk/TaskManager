using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.API.DTOs
{
    public class GlobalTaskDTO : TaskDTO
    {
        public DateTime FinishDate { get; set; }
        public List<SubTaskDTO> SubTasks { get; set; }
    }
}
