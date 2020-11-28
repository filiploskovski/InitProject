using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DbInit.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext dataContext;

        public Repository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<List<T>> Get()
        {
            return await dataContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dataContext.Set<T>().FindAsync(id);
        }

        public async Task<List<U>> GetAllBy<U>(Func<IQueryable<T>, IQueryable<U>> queryCallback)
        {
            return await dataContext.Set<T>().AsQueryable<T>().Provider.CreateQuery<U>(queryCallback(dataContext.Set<T>()).Expression).ToListAsync();
        }

        public async Task<U> GetFirstBy<U>(Func<IQueryable<T>, IQueryable<U>> queryCallback)
        {
            return await dataContext.Set<T>().AsQueryable<T>().Provider.CreateQuery<U>(queryCallback(dataContext.Set<T>()).Expression).FirstOrDefaultAsync();
        }

        public async Task<bool> HasAnyBy<U>(Func<IQueryable<T>, IQueryable<U>> queryCallback)
        {
            return await dataContext.Set<T>().AsQueryable<T>().Provider.CreateQuery<U>(queryCallback(dataContext.Set<T>()).Expression).AnyAsync();
        }

        public async Task<bool> HasAny()
        {
            return await dataContext.Set<T>().AnyAsync();
        }

        public async Task<int> CountAll()
        {
            return await dataContext.Set<T>().CountAsync();
        }

        public async Task<int> CountBy<U>(Func<IQueryable<T>, IQueryable<U>> queryCallback)
        {
            return await dataContext.Set<T>().AsQueryable<T>().Provider.CreateQuery<U>(queryCallback(dataContext.Set<T>()).Expression).CountAsync();
        }

        public async Task<T> Create(T entity)
        {
            await dataContext.Set<T>().AddAsync(entity);
            await dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task CreateRange(List<T> entities)
        {
            await dataContext.Set<T>().AddRangeAsync(entities);
            await dataContext.SaveChangesAsync();

            await Task.FromResult(entities);
        }

        public async Task<T> Update(T entity)
        {
            dataContext.Set<T>().Update(entity);
            await dataContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            dataContext.Set<T>().Remove(entity);
            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteRange(List<T> entities, bool saveChanges)
        {
            if (!entities.Any()) return;
            dataContext.Set<T>().RemoveRange(entities);
            if (saveChanges)
                await dataContext.SaveChangesAsync();
        }
    }
}