using MangasAPI.Entities;

namespace MangasAPI.Repositories.Interfaces
{
    public interface IMangaRepository : IRepository<Manga>
    {
        Task<IEnumerable<Manga>> GetMangasPorCategoriaAsync(int categoriaId);
        Task<IEnumerable<Manga>> LocalizarMangasComCategoriaAsync(string criterio);
    }
}
