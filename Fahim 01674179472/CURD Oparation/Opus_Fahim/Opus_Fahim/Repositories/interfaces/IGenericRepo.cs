using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Opus_Fahim.Repositories.interfaces
{
    public interface IGenericRepo<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> predicate);

    }
}
