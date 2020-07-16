using System;

namespace TaskManager.Core.Messaging
{
    public interface IMessage
    {
        public Guid Id { get; }
    }
}
