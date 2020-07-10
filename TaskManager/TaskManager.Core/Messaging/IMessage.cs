using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Messaging
{
    public interface IMessage
    {
        public Guid Id { get; }
    }
}
