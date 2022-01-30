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
    public class AlunoController : ControllerBase
    {
        public readonly IRepository Repository;
        public readonly IMapper Mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAsync([FromQuery]PaginationParams pageParams)
        {
            var alunos = await Repository.GetAlunos(pageParams, true);
            var alunosMapped = Mapper.Map<IEnumerable<AlunoDto>>(alunos);

            Response.AddPagination(alunos.CurrentPage, alunos.PageSize, alunos.TotalCount, alunos.TotalPages);

            return Ok(alunosMapped);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetByIdAsync(int id)
        {
            var aluno = await Repository.GetAluno(id, true);
            return Ok(Mapper.Map<AlunoDto>(aluno));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, AlunoDto alunoDto)
        {
            if (alunoDto.Id == id) return BadRequest("Identificador diferente da entidade");

            var aluno = await Repository.GetProfessor(id, false);
            if (aluno == null) return BadRequest("Entidade não encontrada");

            Mapper.Map(alunoDto, aluno);

            Repository.Update(aluno);
            if (await Repository.SaveChangesAsync())
            {
                return Ok("Alterado!");
            }
            return BadRequest("Erro");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(AlunoDto alunoDto)
        {
            var aluno = Mapper.Map<Aluno>(alunoDto);
            await Repository.AddAsync(aluno);
            if (await Repository.SaveChangesAsync())
            {
                return Created($"api/aluno/{alunoDto.Id}", Mapper.Map<AlunoDto>(aluno));
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