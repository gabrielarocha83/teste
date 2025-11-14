using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yara.Domain.Repository;

namespace Yara.Data.Entity.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAllFilterAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllOrderAndPaginationAsync<TKey>(Expression<Func<T, bool>> expression, Expression<Func<T, TKey>> order, int Page, int Skip, bool OrderAscending)
        {
            var filter = _context.Set<T>().Where(expression);
            filter = OrderAscending ? filter.OrderBy(order) : filter.OrderByDescending(order);
            return await filter.Take(Page).Skip(Skip).ToListAsync();
        }

        public void Insert(T obj)
        {
            _context.Set<T>().Add(obj);
        }

        public void Update(T obj)
        {
            _context.Set<T>().Attach(obj);
            _context.Entry<T>(obj).State = EntityState.Modified;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
