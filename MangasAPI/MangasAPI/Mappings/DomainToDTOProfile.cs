using AutoMapper;
using MangasAPI.DTOs;
using MangasAPI.Entities;

namespace MangasAPI.Mappings
{
    public class DomainToDTOProfile : Profile
    {
        public DomainToDTOProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Manga, MangaDTO>().ReverseMap();

            CreateMap<Manga, MangaCategoriaDTO>().ForMember(x => x.NomeCategoria, x => x.MapFrom(x => x.Categoria.Nome));
        }
    }
}
