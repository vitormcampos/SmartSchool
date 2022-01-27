using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Dtos
{
    public class DisciplinaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Curso Curso { get; set; }
        public int CursoId { get; set; }
    }
}