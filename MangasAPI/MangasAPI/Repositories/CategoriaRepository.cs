using MangasAPI.Context;
using MangasAPI.Entities;
using MangasAPI.Repositories.Interfaces;

namespace MangasAPI.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext db) : base(db) { }
    }
}
