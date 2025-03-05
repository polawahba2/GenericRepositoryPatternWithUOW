using System.Linq.Expressions;
using LibraryManagementSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;

        public GenericRepository(ApplicationDBContext context)
            => _context = context;

        public async Task<T?> GetByIDAsync(int id)
            => await _context.Set<T>().FindAsync(id);

        public async Task<T?> GetByIDAsync(int id, string[] includes)
        {
            // using reflection to get thr id property of this entity
            var keyName = _context.Model.FindEntityType(typeof(T))?
            .FindPrimaryKey()?.Properties
            .Select(p => p.Name)
            .FirstOrDefault();

            if (keyName == null) return null;

            IQueryable<T> query = _context.Set<T>();
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();
        public async Task<IEnumerable<T>> GetAllAsync(string[] includes)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync();

        }

        public async Task<IEnumerable<T>> FindByCriteriaAsync(Expression<Func<T, bool>> criteria)
            => await _context.Set<T>().Where(criteria).ToListAsync();

        public async Task<IEnumerable<T>> FindByCriteriaAsync(Expression<Func<T, bool>> criteria, string[]? includes = null, int? skip = null, int? take = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = query.Where(criteria);

            if (skip.HasValue && take.HasValue)
            {
                if (skip.Value < 0) skip = 0;
                if (take.Value <= 0) take = 10; // Default to 10 items per page if take is not set properly

                query = query.Skip(skip.Value).Take(take.Value);
            }

            return await query.ToListAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }


    }
}