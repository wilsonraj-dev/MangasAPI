using MangasAPI.Context;
using MangasAPI.Entities;
using MangasAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MangasAPI.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class MangaRepository : Repository<Manga>, IMangaRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public MangaRepository(AppDbContext context) : base(context) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Manga>> GetMangasPorCategoriaAsync(int categoriaId)
        {
            return await _db.Mangas.Where(b => b.CategoriaId == categoriaId).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criterio"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Manga>> LocalizarMangasComCategoriaAsync(string criterio)
        {
            return await _db.Mangas.AsNoTracking()
                                   .Include(x => x.Categoria)
                                   .Where(x => x.Titulo.Contains(criterio) ||
                                               x.Autor.Contains(criterio) ||
                                               x.Descricao.Contains(criterio) ||
                                               x.Categoria.Nome.Contains(criterio)).ToListAsync();
        }
    }
}
