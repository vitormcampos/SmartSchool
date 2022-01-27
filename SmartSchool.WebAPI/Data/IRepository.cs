using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        Task AddAsync<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        /// Alunos
        Task<IEnumerable<Aluno>> GetAlunos(bool includeProfessor = false);
        Task<IEnumerable<Aluno>> GetAlunosByDisciplinaId(int id, bool includeProfessor = false);
        Task<Aluno> GetAluno(int id, bool includeProfessor = false);

        /// Professores
        Task<IEnumerable<Professor>> GetProfessores(bool includeAluno = false);
        Task<IEnumerable<Professor>> GetProfessoresByDisciplinaId(int id, bool includeAluno = false);
        Task<Professor> GetProfessor(int id, bool includeAluno = false);


        /// Disciplinas
        Task<IEnumerable<Disciplina>> GetDisciplinas();
        Task<Disciplina> GetDisciplina(int id);


        /// Cursos
        Task<IEnumerable<Curso>> GetCursos();
        Task<Curso> GetCurso(int id);
    }
}