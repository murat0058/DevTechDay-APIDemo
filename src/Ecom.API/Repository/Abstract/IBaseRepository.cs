using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ecom.API.Abstract.Repository
{
    public interface IBaseRepository<T> where T: class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetSingle(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void Commit();
    }
}
