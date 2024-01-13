using Opus_Fahim.Models;
using Opus_Fahim.Repositories.interfaces;

namespace Opus_Fahim.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private EmployeeDbContext db;
        public UnitOfWork(EmployeeDbContext db)
        {
            this.db = db;
        }
        public IGenericRepo<T> GetRepo<T>() where T : class, new()
        {
            return new GenericRepo<T>(db);
        }

        public async Task<bool> SaveAsync()
        {
            int rowsEffected = await db.SaveChangesAsync();
            return rowsEffected > 0;
        }
    }
}
