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
    public class ProfessorController : ControllerBase
    {
        public readonly IRepository Repository;

        public ProfessorController(IRepository repository)
        {
            this.Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetAsync()
        {
            var professores = await Repository.GetProfessores(true);
            return Ok(professores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetByIdAsync(int id)
        {
            var professor = await Repository.GetProfessor(id, true);
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Professor professor)
        {
            if (professor.Id == id) return BadRequest("Identificador diferente da entidade");

            var _professor = await Repository.GetProfessor(id, false);
            if (_professor == null) return BadRequest("Entidade não encontrada");

            Repository.Update(professor);
            if (await Repository.SaveChangesAsync())
            {
                return Ok("Alterado!");
            }
            return BadRequest("Erro");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Professor professor)
        {
            await Repository.AddAsync(professor);
            if (await Repository.SaveChangesAsync())
            {
                return Created($"api/professor/{professor.Id}", professor);
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