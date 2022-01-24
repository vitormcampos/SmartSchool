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
        public Context Context { get; }

        public ProfessorController(Context context)
        {
            this.Context = context;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professor>>> GetAsync()
        {
            var professores = await Context.Professores
                                            .AsNoTracking()
                                            .ToListAsync();
            return Ok(professores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Professor>> GetByIdAsync(int id)
        {
            var professor = await Context.Professores
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(p => p.Id == id);
            return Ok(professor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Professor professor)
        {
            var _professor = await Context.Professores.FirstOrDefaultAsync(p => p.Id == id);
            professor.Id = _professor.Id;
            Context.Professores.Update(professor);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Professor professor)
        {
            Repository.Professores.Add(professor);
            return Created($"api/aluno/{professor.Id}", professor);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var professor = Repository.Professores.FirstOrDefault(p => p.Id == id);
            return Ok(Repository.Professores.Remove(professor));
        }

    }
}