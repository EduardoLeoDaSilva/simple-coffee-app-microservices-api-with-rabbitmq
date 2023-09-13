using CoffeeOnDemandSolution.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CoffeeOnDemandSolution.Common.Data.Repositories
{
    public class BaseRepository<Context,E> where Context : DbContext where E : class
    {
        private readonly Context _context;
        public BaseRepository(Context context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Create(E e)
        {
            await _context.AddAsync(e);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<E> GetById(Guid id)
        {
            var dbRecord = _context.Set<E>().Find(id);
            return dbRecord;
        }

        public async Task<List<E>> Query(Expression<Func<E, bool>> expression = null, params string[] includes)
        {

            var dbset = _context.Set<E>();
            var query = dbset.AsQueryable();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            if (expression != null)
                query = query.Where(expression);

            return query.ToList();
        }

        public async Task<bool> Update(E e)
        {
             _context.Update(e);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entityName = typeof(E);
            var dbRecords = await _context.FindAsync(entityName, id);
            if (dbRecords == null)
                return false;

             _context.Remove(dbRecords);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

    }
}
