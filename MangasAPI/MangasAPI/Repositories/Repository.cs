using MangasAPI.Context;
using MangasAPI.Entities;
using MangasAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MangasAPI.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly AppDbContext _db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(AppDbContext db)
        {
            _db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int? id)
        {
            return await DbSet.FindAsync(id) ?? throw new ArgumentException();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            DbSet.Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task RemoveAsync(int? id)
        {
            var entity = await DbSet.FindAsync(id);
            DbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db?.Dispose();
        }
    }
}
