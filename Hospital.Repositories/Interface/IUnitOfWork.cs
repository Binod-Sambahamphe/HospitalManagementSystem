namespace Hospital.Repositories.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<T>GenericRepository<T>() where T : class;
        void Save();
    }
}
