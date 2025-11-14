using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Yara.Domain.Repository
{
    public interface IRepositoryBase<T> : IDisposable where T : class
    {
        Task<T> GetAsync(Expression<Func<T,bool>> expression);
        Task<IEnumerable<T>> GetAllFilterAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        void Insert(T obj);
        void Update(T obj);
    }
}
