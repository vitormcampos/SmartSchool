using System.Collections.Generic;
using System.Threading.Tasks;
using SmartSchool.WebAPI.Helpers;
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
        Task<PaginationList<Aluno>> GetAlunos(PaginationParams pageParams, bool includeProfessor = false);
        Task<PaginationList<Aluno>> GetAlunosByDisciplinaId(int id, PaginationParams pageParams, bool includeProfessor = false);
        Task<Aluno> GetAluno(int id, bool includeProfessor = false);

        /// Professores
        Task<PaginationList<Professor>> GetProfessores(PaginationParams pageParams, bool includeAluno = false);
        Task<PaginationList<Professor>> GetProfessoresByDisciplinaId(int id, PaginationParams pageParams, bool includeAluno = false);
        Task<Professor> GetProfessor(int id, bool includeAluno = false);


        /// Disciplinas
        Task<PaginationList<Disciplina>> GetDisciplinas(PaginationParams pageParams);
        Task<Disciplina> GetDisciplina(int id);


        /// Cursos
        Task<PaginationList<Curso>> GetCursos(PaginationParams pageParams);
        Task<Curso> GetCurso(int id);
    }
}