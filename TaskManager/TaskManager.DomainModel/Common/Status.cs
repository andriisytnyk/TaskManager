using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.DomainModel.Common
{
    public enum Status
    {
        Undefined = 0,
        ToDo,
        InProgress,
        Done,
        Canceled
    }
}
