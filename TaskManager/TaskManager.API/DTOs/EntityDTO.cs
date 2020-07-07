using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.API.DTOs
{
    public abstract class EntityDTO
    {
        public int Id { get; set; }

        public bool IsNew => Id == default(int);
    }
}
