using System;

namespace TaskManager.Core.Repositories
{
    public interface ITransaction : IDisposable
    {
        void Rollback();

        void Commit();
    }
}
