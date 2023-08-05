using AutoMapper;
using MangasAPI.DTOs;
using MangasAPI.Entities;
using MangasAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangasAPI.Controllers
{
    /// <summary>
    /// CategoriasController
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="categoryRepository"></param>
        /// <param name="mapper"></param>
        public CategoriasController(ICategoriaRepository categoryRepository,
            IMapper mapper)
        {
            _categoriaRepository = categoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna uma lista com as categorias cadastradas
        /// </summary>
        /// <returns>As categorias encontrada</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            if (categorias is null)
            {
                return NotFound("Categorias não existem");
            }

            var categoriasDto = _mapper.Map<IEnumerable<CategoriaDTO>>(categorias);
            return Ok(categoriasDto);
        }

        /// <summary>
        /// Obtem uma categoria pelo seu Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A categoria encontrada</returns>
        [HttpGet("{id:int}", Name = "GetCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria is null)
            {
                return NotFound("Categoria não encontrada");
            }

            var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);
            return Ok(categoriaDto);
        }

        /// <summary>
        /// Cadastra uma nova categoria
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     POST Categorias
        ///     {
        ///         "nome": "Shounen"
        ///     }
        /// </remarks>
        /// <param name="categoriaDto">Objeto categoriaDTO</param>
        /// <returns>Criação de uma nova categoria</returns>
        /// <remarks>Retorna o objeto Categoria incluído</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] CategoriaDTO categoriaDto)
        {
            if (categoriaDto == null)
            {
                return BadRequest("Dados inválidos");
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);
            await _categoriaRepository.AddAsync(categoria);

            return new CreatedAtRouteResult("GetCategoria", new { id = categoriaDto.Id }, categoriaDto);
        }

        /// <summary>
        /// Atualiza uma categoria
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     PUT Categorias
        ///     {
        ///         "id": 1,
        ///         "nome": "Romance"
        ///     }
        /// </remarks>
        /// <param name="id">Id da categoria</param>
        /// <param name="categoriaDto">Objeto CategoriaDTO</param>
        /// <returns>O objeto categoria atualizado</returns>
        /// <remarks>O objeto categoria atualizado</remarks>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(int id, [FromBody] CategoriaDTO categoriaDto)
        {
            if (id != categoriaDto.Id)
            {
                return BadRequest();
            }
            if (categoriaDto == null)
            {
                return BadRequest();
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);
            await _categoriaRepository.UpdateAsync(categoria);

            return Ok(categoriaDto);
        }

        /// <summary>
        /// Remove uma categoria
        /// </summary>
        /// <param name="id">Id da categoria</param>
        /// <returns>Objeto categoria que foi removido</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada");
            }

            await _categoriaRepository.RemoveAsync(id);
            return Ok(categoria);
        }
    }
}
