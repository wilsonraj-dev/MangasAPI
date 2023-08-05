using MangasAPI.Context;
using MangasAPI.Entities;
using MangasAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MangasAPI.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly AppDbContext _db;

        /// <summary>
        /// 
        /// </summary>
        protected readonly DbSet<TEntity> DbSet;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        protected Repository(AppDbContext db)
        {
            _db = db;
            DbSet = db.Set<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<TEntity> GetByIdAsync(int? id)
        {
            return await DbSet.FindAsync(id) ?? throw new ArgumentException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task RemoveAsync(int? id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
