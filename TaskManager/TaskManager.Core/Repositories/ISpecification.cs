using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TaskManager.Core.Repositories
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; }

        public List<string> IncludeStrings { get; }

        public Expression<Func<T, object>> OrderBy { get; }

        public Expression<Func<T, object>> OrderByDescending { get; }

        public Expression<Func<T, object>> GroupBy { get; }

        public int Take { get; }

        public int Skip { get; }

        public bool IsPagingEnabled { get; }
    }
}
