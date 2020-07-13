﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.API.DTOs
{
    public abstract class DescriptiveEntityDTO : EntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}