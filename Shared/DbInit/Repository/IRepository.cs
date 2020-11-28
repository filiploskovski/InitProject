using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.DbInit.Repository
{
    public interface IRepository<T> where T : class
    {
        #region Query Data

        Task<List<T>> Get();
        Task<T> GetById(int id);
        Task<List<U>> GetAllBy<U>(Func<IQueryable<T>, IQueryable<U>> queryCallback);
        Task<U> GetFirstBy<U>(Func<IQueryable<T>, IQueryable<U>> queryCallback);

        #endregion



        #region Any

        Task<bool> HasAnyBy<U>(Func<IQueryable<T>, IQueryable<U>> queryCallback);
        Task<bool> HasAny();

        #endregion



        #region COUNT

        Task<int> CountAll();
        Task<int> CountBy<U>(Func<IQueryable<T>, IQueryable<U>> queryCallback);

        #endregion



        #region CRUD

        Task<T> Create(T entity);
        Task CreateRange(List<T> entities);
        Task<T> Update(T entity);
        Task Delete(T entity);
        Task DeleteRange(List<T> entities, bool saveChanges);

        #endregion
    }
}