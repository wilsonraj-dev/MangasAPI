using MangasAPI.Entities;
using System.Linq.Expressions;

namespace MangasAPI.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task<T> AddAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int? id);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(int id);

        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> predicate);
    }
}
