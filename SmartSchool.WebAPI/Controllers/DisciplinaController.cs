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
        public Context Context { get; }

        public DisciplinaController(Context context)
        {
            this.Context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplina>>> GetAsync()
        {
            var disciplinas = await Context.Disciplinas
                                        .AsNoTracking()
                                        .ToListAsync();
            return Ok(disciplinas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Disciplina>> GetByIdAsync(int id)
        {
            var disciplina = await Context.Disciplinas
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(d => d.Id == id);
            return Ok(disciplina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Disciplina disciplina)
        {
            var _disciplina = await Context.Disciplinas.FirstOrDefaultAsync(d => d.Id == id);
            disciplina.Id = _disciplina.Id;
            Context.Update(disciplina);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Disciplina disciplina)
        {
            await Context.Disciplinas.AddAsync(disciplina);
            await Context.SaveChangesAsync();
            return Created($"api/aluno/{disciplina.Id}", disciplina);
        }
        
    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var disciplina = await Context.Disciplinas.FirstOrDefaultAsync(d => d.Id == id);
            Context.Disciplinas.Remove(disciplina);
            await Context.SaveChangesAsync();
            return Ok();
        }
        
    }
}