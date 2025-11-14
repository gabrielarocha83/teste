using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceBase<T> where T:class
    {
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllFilterAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAllAsync();
        bool Insert(T obj);
        Task<bool> Update(T obj);
    }
}