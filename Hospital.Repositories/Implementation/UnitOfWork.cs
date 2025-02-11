using Hospital.Repositories.Interface;
using Microsoft.Extensions.Logging;

namespace Hospital.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILoggerFactory _loggerFactory;

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "ApplicationDbContext cannot be null.");
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory), "ILoggerFactory cannot be null.");
        }

        private bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            var logger = _loggerFactory.CreateLogger<GenericRepository<T>>();
            return new GenericRepository<T>(_context, logger);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
