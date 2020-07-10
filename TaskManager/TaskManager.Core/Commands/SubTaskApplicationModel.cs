using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.DomainModel.Common;

namespace TaskManager.Core.Commands
{
    public class SubTaskApplicationModel
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public Status Status { get; }

        public SubTaskApplicationModel(int id, string name, string desc, Status status)
        {
            Id = id;
            Name = name;
            Description = desc;
            Status = status;
        }
    }
}
