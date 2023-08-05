using MangasAPI.Entities;

namespace MangasAPI.Repositories.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMangaRepository : IRepository<Manga>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoriaId"></param>
        /// <returns></returns>
        Task<IEnumerable<Manga>> GetMangasPorCategoriaAsync(int categoriaId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criterio"></param>
        /// <returns></returns>
        Task<IEnumerable<Manga>> LocalizarMangasComCategoriaAsync(string criterio);
    }
}
