using Hospital.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Hospital.Repositories.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly ILogger<GenericRepository<T>> _logger;

        public GenericRepository(DbContext context, ILogger<GenericRepository<T>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "DbContext cannot be null.");
            _dbSet = _context.Set<T>() ?? throw new ArgumentNullException(nameof(_dbSet), "DbSet cannot be null.");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger), "Logger cannot be null.");
        }

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _dbSet.AsQueryable();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (!string.IsNullOrWhiteSpace(includeProperties))
                {
                    foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (typeof(T).GetProperty(includeProperty.Trim()) != null)
                        {
                            query = query.Include(includeProperty.Trim());
                        }
                        else
                        {
                            _logger.LogWarning($"Invalid include property: {includeProperty}");
                        }
                    }
                }

                return orderBy != null ? orderBy(query).ToList() : query.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAll: {ex.Message}", ex);
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _dbSet.AsQueryable();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (!string.IsNullOrWhiteSpace(includeProperties))
                {
                    foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    {
                        if (typeof(T).GetProperty(includeProperty.Trim()) != null)
                        {
                            query = query.Include(includeProperty.Trim());
                        }
                        else
                        {
                            _logger.LogWarning($"Invalid include property: {includeProperty}");
                        }
                    }
                }

                return orderBy != null ? await orderBy(query).ToListAsync() : await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllAsync: {ex.Message}", ex);
                throw;
            }
        }

        public T GetById(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "ID cannot be null.");
            return _dbSet.Find(id);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "ID cannot be null.");
            return await _dbSet.FindAsync(id);
        }

        public void Insert(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            _dbSet.Add(entity);
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            await _dbSet.AddAsync(entity);
        }

        public void Delete(object id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "ID cannot be null.");
            var entityToDelete = _dbSet.Find(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in SaveAsync: {ex.Message}", ex);
                throw;
            }
        }

        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Save: {ex.Message}", ex);
                throw;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Add(T entity)
        {
            if (entity == null)
           {
               throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
           }
            _dbSet.Add(entity);
        }
    }
}
