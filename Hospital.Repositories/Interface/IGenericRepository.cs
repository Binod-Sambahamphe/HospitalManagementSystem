using System.Linq.Expressions;

namespace Hospital.Repositories.Interface
{
    // Generic repository interface for managing data operations
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        // Retrieve all entities, optionally applying filters and include properties
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        // Retrieve an entity by its identifier
        T GetById(object id);

        // Insert a new entity
        void Insert(T entity);

        // Delete an entity by its identifier
        void Delete(object id);

        // Delete an entity by its instance
        void Delete(T entity);

        // Update an existing entity
        void Update(T entity);

        void Add(T entity);

        // Save changes to the data store
        void Save();
    }
}
