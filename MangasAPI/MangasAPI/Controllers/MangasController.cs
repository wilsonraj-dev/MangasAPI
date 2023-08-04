﻿using AutoMapper;
using MangasAPI.DTOs;
using MangasAPI.Entities;
using MangasAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MangasAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MangasController : ControllerBase
    {
        private readonly IMangaRepository _mangaRepository;
        private readonly IMapper _mapper;

        public MangasController(IMangaRepository mangaRepository,
            IMapper mapper)
        {
            _mangaRepository = mangaRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var mangas = await _mangaRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<MangaDTO>>(mangas));
        }

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add(MangaDTO mangaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var manga = _mapper.Map<Manga>(mangaDto);
            await _mangaRepository.AddAsync(manga);

            return Ok(_mapper.Map<MangaDTO>(manga));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, MangaDTO mangaDto)
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
