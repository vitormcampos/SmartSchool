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
        public Context Context { get; }

        public AlunoController(Context context)
        {
            this.Context = context;
        }

        [HttpGet]
        public async  Task<ActionResult<IEnumerable<Aluno>>> GetAsync()
        {
            var alunos = await Context.Alunos
                                .AsNoTracking()
                                .ToListAsync();
            return Ok(alunos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetByIdAsync(int id)
        {
            var aluno = await Context.Alunos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(a => a.Id == id);
            return Ok(aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, Aluno aluno)
        {
            var _aluno = await Context.Alunos.FirstOrDefaultAsync(a => a.Id == id);
            aluno.Id = _aluno.Id;
            Context.Alunos.Update(aluno);
            await Context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Aluno aluno)
        {
            await Context.AddAsync(aluno);
            await Context.SaveChangesAsync();
            return Created($"api/aluno/{aluno.Id}", aluno);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var aluno = await Context.Alunos.FirstOrDefaultAsync(a => a.Id == id);
            Context.Alunos.Remove(aluno);
            await Context.SaveChangesAsync();
            return Ok();
        }

    }
}