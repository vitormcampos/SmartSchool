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
    public class DisciplinaController : ControllerBase
    {

        public readonly Repository Repository;
        public DisciplinaController(Repository repository)
        {
            this.Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplina>>> GetAsync()
        {
            var disciplinas = await Repository.GetDisciplinas();
            return Ok(disciplinas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Disciplina>> GetByIdAsync(int id)
        {
            var disciplina = await Repository.GetDisciplina(id);
            return Ok(disciplina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Disciplina disciplina)
        {
            if (disciplina.Id == id) return BadRequest("Identificador diferente da entidade");

            var _disciplina = await Repository.GetDisciplina(id);
            if (_disciplina == null) return BadRequest("Entidade não encontrada");

            Repository.Update(disciplina);
            if (await Repository.SaveChangesAsync())
            {
                return Ok("Alterado!");
            }
            return BadRequest("Erro");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Disciplina disciplina)
        {
            await Repository.AddAsync(disciplina);
            if (await Repository.SaveChangesAsync())
            {
                return Created($"api/disciplina/{disciplina.Id}", disciplina);
            }
            return BadRequest("Erro");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var disciplina = await Repository.GetDisciplina(id);

            if (disciplina == null) return BadRequest("Entidade não encontrada");

            Repository.Delete(disciplina);
            if (await Repository.SaveChangesAsync())
            {
                return Ok("Deletado!");
            }
            return BadRequest("Erro");
        }

    }
}