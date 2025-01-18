using Hospital.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hospital.Repositories.Implementation
{
      public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            private readonly DbContext _context;
            private readonly DbSet<T> _dbSet;

            public GenericRepository(DbContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public IEnumerable<T> GetAll(
                Expression<Func<T, bool>> filter = null,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                string includeProperties = "")
            {
                IQueryable<T> query = _dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }

                return query.ToList();
            }

            public T GetById(object id)
            {
                return _dbSet.Find(id);
            }

            public void Insert(T entity)
            {
                _dbSet.Add(entity);
            }

            public void Delete(object id)
            {
                T entityToDelete = _dbSet.Find(id);
                if (entityToDelete != null)
                {
                    Delete(entityToDelete);
                }
            }

            public void Delete(T entity)
            {
                if (_context.Entry(entity).State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                }
                _dbSet.Remove(entity);
            }

            public void Update(T entity)
            {
                _dbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
            }

            public void Save()
            {
                _context.SaveChanges();
            }

            public void Dispose()
            {
                _context.Dispose();
            }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }
    }
    }

