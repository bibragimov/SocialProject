using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SocialProject.DAL.Core.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> All();

        void Insert(T item);

        T Get(long id);

        T Query(Expression<Func<T, bool>> filter = null);

        void Update(T item);

        void Delete(long id);

        void Delete(T item);

        void SaveChanges();
    }
}