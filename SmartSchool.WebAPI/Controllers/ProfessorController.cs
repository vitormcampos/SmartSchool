using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfessorController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Professor>> Get()
        {
            return Ok(Repository.Professores);
        }

        [HttpGet("{id}")]
        public ActionResult<Professor> GetById(int id)
        {
            return Ok(Repository.Professores.FirstOrDefault(p => p.Id == id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Professor professor)
        {
            var _professor = Repository.Professores.FirstOrDefault(p => p.Id == id);
            professor.Id = _professor.Id;
            Repository.Professores.Remove(professor);
            Repository.Professores.Add(professor);
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