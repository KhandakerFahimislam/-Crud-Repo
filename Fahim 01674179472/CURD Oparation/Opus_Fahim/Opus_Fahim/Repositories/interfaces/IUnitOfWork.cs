namespace Opus_Fahim.Repositories.interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepo<T> GetRepo<T>() where T : class, new();
        Task<bool> SaveAsync();
    }
}
