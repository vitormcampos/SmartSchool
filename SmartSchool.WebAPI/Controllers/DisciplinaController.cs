using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Disciplina>> Get()
        {
            var disciplinas = Repository.Disciplinas;
            return Ok(disciplinas);
        }

        [HttpGet("{id}")]
        public ActionResult<Disciplina> GetById(int id)
        {
            var disciplina = Repository.Disciplinas.FirstOrDefault(d => d.Id == id);
            return Ok(disciplina);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Disciplina disciplina)
        {
            var _disciplina = Repository.Disciplinas.FirstOrDefault(d => d.Id == id);
            disciplina.Id = _disciplina.Id;
            Repository.Disciplinas.Remove(disciplina);
            Repository.Disciplinas.Add(disciplina);
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Disciplina disciplina)
        {
            Repository.Disciplinas.Add(disciplina);
            return Created($"api/aluno/{disciplina.Id}", disciplina);
        }
        
    [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var disciplina = Repository.Disciplinas.FirstOrDefault(d => d.Id == id);
            return Ok(Repository.Disciplinas.Remove(disciplina));
        }
        
    }
}