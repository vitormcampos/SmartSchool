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
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository Repository;
        public readonly IMapper Mapper;

        public ProfessorController(IRepository repository, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetAsync([FromQuery] PaginationParams pageParams)
        {
            var professores = await Repository.GetProfessores(pageParams, true);
            var professoresMapped = Mapper.Map<IEnumerable<ProfessorDto>>(professores);
            Response.AddPagination(professores.CurrentPage, professores.PageSize, professores.TotalCount, professores.TotalPages);

            return Ok(professoresMapped);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetByIdAsync(int id)
        {
            var professor = await Repository.GetProfessor(id, true);
            return Ok(Mapper.Map<Professor>(professor));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, ProfessorDto professorDto)
        {
            if (professorDto.Id == id) return BadRequest("Identificador diferente da entidade");

            var professor = await Repository.GetProfessor(id, false);
            if (professor == null) return BadRequest("Entidade não encontrada");

            Mapper.Map(professorDto, professor);

            Repository.Update(professor);
            if (await Repository.SaveChangesAsync())
            {
                return Created($"/api/professor/{professor.Id}", Mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Erro");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(ProfessorDto professorDto)
        {
            var professor = Mapper.Map<Professor>(professorDto);
            await Repository.AddAsync(professor);
            if (await Repository.SaveChangesAsync())
            {
                return Created($"api/professor/{professor.Id}", Mapper.Map<ProfessorDto>(professor));
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