using MangasAPI.Context;
using MangasAPI.Entities;
using MangasAPI.Repositories.Interfaces;

namespace MangasAPI.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public CategoriaRepository(AppDbContext db) : base(db) { }
    }
}
