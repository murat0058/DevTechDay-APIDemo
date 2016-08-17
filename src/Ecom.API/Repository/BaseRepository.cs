using Ecom.API.Abstract.Repository;
using Ecom.API.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Ecom.API.Contexts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace Ecom.API.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class, IEntityBase, new()
    {

        private BaseContext _context;

        public BaseRepository(BaseContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry(entity);
                _context.Set<T>().Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(T entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IQueryable<T> entites = _context.Set<T>().Where(predicate);
                foreach (var entity in entites)
                {
                    _context.Entry<T>(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>().AsNoTracking().AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().AsNoTracking().Where(predicate).AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public T GetSingle(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return _context.Set<T>().AsNoTracking().Where(predicate).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(T entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry(entity);
                dbEntityEntry.State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
