using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Core.Repositories
{
    public interface ITransaction
    {
        void Rollback();

        void Commit();
    }
}
