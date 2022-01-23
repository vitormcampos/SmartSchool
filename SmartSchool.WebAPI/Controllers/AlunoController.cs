using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlunoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Aluno>> Get()
        {
            return Ok(Repository.Alunos);
        }

        [HttpGet("{id}")]
        public ActionResult<Aluno> GetById(int id)
        {
            return Ok(Repository.Alunos.FirstOrDefault(a => a.Id == id));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            var _aluno = Repository.Alunos.FirstOrDefault(a => a.Id == id);
            aluno.Id = _aluno.Id;
            Repository.Alunos.Remove(aluno);
            Repository.Alunos.Add(aluno);
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            Repository.Alunos.Add(aluno);
            return Created($"api/aluno/{aluno.Id}", aluno);
        }
        
    [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var aluno = Repository.Alunos.FirstOrDefault(a => a.Id == id);
            return Ok(Repository.Alunos.Remove(aluno));
        }
        
    }
}