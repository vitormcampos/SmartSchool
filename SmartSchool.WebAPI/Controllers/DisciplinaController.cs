using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Data;
using SmartSchool.WebAPI.Dtos;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplinaController : ControllerBase
    {
        public readonly IMapper Mapper;

        public readonly IRepository Repository;
        public DisciplinaController(IRepository repository, IMapper mapper)
        {
            this.Mapper = mapper;
            this.Repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplina>>> GetAsync()
        {
            var disciplinas = await Repository.GetDisciplinas();
            return Ok(Mapper.Map<IEnumerable<DisciplinaDto>>(disciplinas));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Disciplina>> GetByIdAsync(int id)
        {
            var disciplina = await Repository.GetDisciplina(id);
            return Ok(Mapper.Map<DisciplinaDto>(disciplina));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, DisciplinaDto disciplinaDto)
        {
            if (disciplinaDto.Id == id) return BadRequest("Identificador diferente da entidade");

            var disciplina = await Repository.GetDisciplina(id);
            if (disciplina == null) return BadRequest("Entidade não encontrada");

            Mapper.Map(disciplinaDto, disciplina);
            Repository.Update(disciplina);
            if (await Repository.SaveChangesAsync())
            {
                return Ok("Alterado!");
            }
            return BadRequest("Erro");
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(DisciplinaDto disciplinaDto)
        {
            var disciplina = Mapper.Map<Disciplina>(disciplinaDto);
            await Repository.AddAsync(disciplina);
            if (await Repository.SaveChangesAsync())
            {
                return Created($"api/disciplina/{disciplina.Id}", Mapper.Map<DisciplinaDto>(disciplina));
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