﻿using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Opus_Fahim.Repositories.interfaces;
using System.Linq.Expressions;

namespace Opus_Fahim.Repositories
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class, new()
    {
        DbContext db;
        DbSet<T> dbSet;
        public GenericRepo(DbContext db)
        {
            this.db = db;
            this.dbSet = this.db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            IQueryable<T> data = this.dbSet;
            if (includes != null)
            {
                data = includes(data);
            }
            return await data.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            IQueryable<T> data = this.dbSet;
            if (includes != null)
            {
                data = includes(data);
            }
            return await data.FirstOrDefaultAsync(predicate);
        }

        public async Task InsertAsync(T entity)
        {
            await this.dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }
        public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            var entity = await this.dbSet.FirstOrDefaultAsync(predicate);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }



    }
}
