using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        public readonly IRepository Repository;
        public readonly IMapper Mapper;

        public CursoController(IRepository repository, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetAsync([FromQuery] PaginationParams pageParams)
        {
            var cursos = await Repository.GetCursos(pageParams);
            var cursosMapped = Mapper.Map<IEnumerable<CursoDto>>(cursos);
            Response.AddPagination(cursos.CurrentPage, cursos.PageSize, cursos.TotalCount, cursos.TotalPages);

            return Ok(cursosMapped);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetByIdAsync(int id)
        {
            var curso = await Repository.GetCurso(id);
            return Ok(Mapper.Map<CursoDto>(curso));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, CursoDto cursoDto)
        {
            if (cursoDto.Id == id) return BadRequest("Identificador diferente da entidade");

            var curso = await Repository.GetCurso(id);
            if (curso == null) return BadRequest("Entidade não encontrada");

            Mapper.Map(cursoDto, curso);

            Repository.Update(curso);
            if (await Repository.SaveChangesAsync())
            {
                return Created($"api/curso/{curso.Id}", Mapper.Map<CursoDto>(curso));
            }
            return BadRequest("Erro");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CursoDto cursoDto)
        {
            var curso = Mapper.Map<Curso>(cursoDto);
            await Repository.AddAsync(curso);
            if (await Repository.SaveChangesAsync())
            {
                return Created($"api/aluno/{cursoDto.Id}", Mapper.Map<CursoDto>(curso));
            }
            return BadRequest("Erro");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var professor = await Repository.GetProfessor(id, false);

            if (professor == null) return BadRequest("Entidade não encontrada");

            Repository.Delete(professor);
            if (await Repository.SaveChangesAsync())
            {
                return Ok("Deletado!");
            }
            return BadRequest("Erro");
        }

    }
}