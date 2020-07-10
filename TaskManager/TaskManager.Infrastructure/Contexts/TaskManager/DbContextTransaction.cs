using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Core.Repositories;

namespace TaskManager.Infrastructure.Contexts.TaskManager
{
    public class DbContextTransaction : ITransaction
    {
        private readonly IDbContextTransaction _transaction;

        public DbContextTransaction(IDbContextTransaction transaction)
        {
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Dispose()
        {
            _transaction.Dispose();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }
    }
}
