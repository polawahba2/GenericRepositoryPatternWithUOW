using System.Linq.Expressions;

namespace LibraryManagementSystem.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<T?> GetByIDAsync(int id);
        public Task<T?> GetByIDAsync(int id, string[] includes);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetAllAsync(string[] includes);
        public Task<IEnumerable<T>> FindByCriteriaAsync(Expression<Func<T, bool>> criteria);
        public Task<IEnumerable<T>> FindByCriteriaAsync(Expression<Func<T, bool>> criteria, string[]? includes = null, int? skip = null, int? take = null);
        public Task<T> AddAsync(T entity);
    }
}