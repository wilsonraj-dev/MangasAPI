using AutoMapper;
using MangasAPI.DTOs;
using MangasAPI.Entities;
using MangasAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangasAPI.Controllers
{
    /// <summary>
    /// MangasController
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class MangasController : ControllerBase
    {
        private readonly IMangaRepository _mangaRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="mangaRepository"></param>
        /// <param name="mapper"></param>
        public MangasController(IMangaRepository mangaRepository,
            IMapper mapper)
        {
            _mangaRepository = mangaRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtem todos os mangas cadastrados
        /// </summary>
        /// <returns>Retorna uma lista de mangas</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var mangas = await _mangaRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<MangaDTO>>(mangas));
        }

        /// <summary>
        /// Obtem um manga pelo seu Id
        /// </summary>
        /// <param name="id">Id do manga</param>
        /// <returns>Retorna o manga encontrado</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var manga = await _mangaRepository.GetByIdAsync(id);

            if (manga is null)
            {
                return NotFound($"Manga com {id} não encontrado");
            }

            return Ok(_mapper.Map<MangaDTO>(manga));
        }

        /// <summary>
        /// Obtem os mangas de acordo com a sua categoria
        /// </summary>
        /// <param name="categoryId">Id da categoria</param>
        /// <returns>Retorna uma lista de mangas</returns>
        [HttpGet]
        [Route("get-mangas-by-category/{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMangasByCategory(int categoryId)
        {
            var mangas = await _mangaRepository.GetMangasPorCategoriaAsync(categoryId);

            if (!mangas.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<MangaDTO>>(mangas));
        }

        /// <summary>
        /// Criação um novo manga
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     POST Mangas
        ///     {
        ///         "titulo": "Dragon Ball",
        ///         "descricao": "Venha conhecer a história de Goku e seus amigos,
        ///         "autor": "Akira Toriyama",
        ///         "editora": "Conrad Editora",
        ///         "paginas": 100,
        ///         "publicacao": 1988,
        ///         "formato": "online",
        ///         "cor": "preto e branco",
        ///         "origem": "Japão",
        ///         "imagem": "dragonBall.png",
        ///         "preco": 9.98,
        ///         "estoque": 264,
        ///         "categoriaId": 1
        ///     }
        /// </remarks>
        /// <param name="mangaDto">Objeto MangaDTO</param>
        /// <returns>Criação de um novo manga</returns>
        /// <remarks>Retorna o objeto Manga incluído</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody] MangaDTO mangaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var manga = _mapper.Map<Manga>(mangaDto);
            await _mangaRepository.AddAsync(manga);

            return Ok(_mapper.Map<MangaDTO>(manga));
        }

        /// <summary>
        /// Atualiza um manga
        /// </summary>
        /// /// <remarks>
        /// Exemplo de request:
        /// 
        ///     PUT Mangas
        ///     {
        ///         "id": 1,
        ///         "titulo": "Dragon Ball",
        ///         "descricao": "Venha conhecer a história de Goku e seus amigos,
        ///         "autor": "Akira Toriyama",
        ///         "editora": "Conrad Editora",
        ///         "paginas": 100,
        ///         "publicacao": 1999,
        ///         "formato": "online",
        ///         "cor": "preto e branco",
        ///         "origem": "Japão",
        ///         "imagem": "dragonBall.png",
        ///         "preco": 9.98,
        ///         "estoque": 264,
        ///         "categoriaId": 1
        ///     }
        /// </remarks>
        /// <param name="id">Id do manga</param>
        /// <param name="mangaDto">Objeto MangaDTO</param>
        /// <returns>Atualização de um novo manga</returns>
        /// <remarks>Retorna o objeto Manga atualizado</remarks>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, [FromBody] MangaDTO mangaDto)
        {
            if (id != mangaDto.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _mangaRepository.UpdateAsync(_mapper.Map<Manga>(mangaDto));
            return Ok(mangaDto);
        }

        /// <summary>
        /// Remove um manga
        /// </summary>
        /// <param name="id">Id do manga</param>
        /// <returns>Retorna o manga removido</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var manga = await _mangaRepository.GetByIdAsync(id);
            if (manga is null)
            {
                return NotFound();
            }

            await _mangaRepository.RemoveAsync(manga.Id);
            return Ok();
        }

        /// <summary>
        /// Obtem o manga pelo seu título
        /// </summary>
        /// <param name="mangaTitulo">Título do manga</param>
        /// <returns>Retorna o(s) manga(s) encontrado(s)</returns>
        [HttpGet]
        [Route("search/{mangaTitulo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MangaDTO>>> Search(string mangaTitulo)
        {
            var mangas = await _mangaRepository.SearchAsync(m => m.Titulo.Contains(mangaTitulo));

            if (mangas is null)
            {
                return NotFound("Nenhum mangá foi encontrado");
            }

            return Ok(_mapper.Map<IEnumerable<MangaDTO>>(mangas));
        }

        /// <summary>
        /// Obtem um manga por algum critério passado → Titulo, Autor, Descricao ou Nome Categoria
        /// </summary>
        /// <param name="criterio">Criterio passado para pesquisa</param>
        /// <returns>Retorna o(s) manga(s) encontrado(s)</returns>
        [HttpGet]
        [Route("search-manga-with-category/{criterio}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<MangaCategoriaDTO>>> SearchMangaWithCategory(string criterio)
        {
            var mangas = _mapper.Map<List<Manga>>(await _mangaRepository.LocalizarMangasComCategoriaAsync(criterio));

            if (!mangas.Any())
            {
                return NotFound("Nenhum mangá foi encontrado");
            }

            return Ok(_mapper.Map<IEnumerable<MangaCategoriaDTO>>(mangas));
        }
    }
}
