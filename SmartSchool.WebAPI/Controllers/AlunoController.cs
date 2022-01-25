using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        public readonly IRepository Repository;

        public AlunoController(IRepository repository)
        {
            this.Repository = repository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAsync()
        {
            var alunos = await Repository.GetAlunos(true);

            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetByIdAsync(int id)
        {
            var aluno = await Repository.GetAluno(id, true);
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Aluno aluno)
        {
            if (aluno.Id == id) return BadRequest("Identificador diferente da entidade");

            var _aluno = await Repository.GetProfessor(id, false);
            if (_aluno == null) return BadRequest("Entidade não encontrada");

            Repository.Update(aluno);
            if (await Repository.SaveChangesAsync())
            {
                return Ok("Alterado!");
            }
            return BadRequest("Erro");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Aluno aluno)
        {
             await Repository.AddAsync(aluno);
            if (await Repository.SaveChangesAsync())
            {
                return Created($"api/aluno/{aluno.Id}", aluno);
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