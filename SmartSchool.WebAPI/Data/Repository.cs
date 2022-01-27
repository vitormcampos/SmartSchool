using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Data
{
    public class Repository : IRepository
    {
        public readonly Context Context;

        public Repository(Context context)
        {
            this.Context = context;

        }
        public async Task AddAsync<T>(T entity) where T : class
        {
            await Context.AddAsync(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await Context.SaveChangesAsync() > 0);
        }

        public void Update<T>(T entity) where T : class
        {
            Context.Update(entity);
        }

        public async Task<Aluno> GetAluno(int id, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = Context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(aluno => aluno.AlunoDisciplinas)
                                .ThenInclude(alunoDisciplina => alunoDisciplina.Disciplina)
                                .ThenInclude(disciplina => disciplina.Professor);
            }
            query = query.AsNoTracking()
                            .Where(aluno => aluno.Id == id);
                return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Aluno>> GetAlunos(bool includeProfessor)
        {
            IQueryable<Aluno> query = Context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(aluno => aluno.AlunoDisciplinas)
                                .ThenInclude(alunoDisciplina => alunoDisciplina.Disciplina)
                                .ThenInclude(disciplina => disciplina.Professor);
            }
            query = query.AsNoTracking()
                            .OrderBy(aluno => aluno.Id);
                return await query.ToListAsync();
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByDisciplinaId(int id, bool includeProfessor = false)
        {
            IQueryable<Aluno> query = Context.Alunos;

            if (includeProfessor)
            {
                query = query.Include(aluno => aluno.AlunoDisciplinas)
                                .ThenInclude(alunoDisciplina => alunoDisciplina.Disciplina)
                                .ThenInclude(disciplina => disciplina.Professor);
            }
            query = query.AsNoTracking()
                            .OrderBy(a => a.Id)
                            .Where(aluno => aluno.AlunoDisciplinas.Any(alunoDisciplinas => alunoDisciplinas.DisciplinaId == id));
                return await query.ToListAsync();
        }

        public async Task<Professor> GetProfessor(int id, bool includeAluno = false)
        {
            IQueryable<Professor> query = Context.Professores;

            if (includeAluno)
            {
                query = query.Include(professor => professor.Disciplinas)
                                .ThenInclude(disciplina => disciplina.AlunoDisciplinas)
                                .ThenInclude(alunoDisciplinas => alunoDisciplinas.Aluno);
                                
            }
            query = query.AsNoTracking()
                            .Where(professor => professor.Id == id);
                return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Professor>> GetProfessores(bool includeAluno)
        {
            IQueryable<Professor> query = Context.Professores;

            if (includeAluno)
            {
                query = query.Include(professor => professor.Disciplinas)
                                .ThenInclude(disciplina => disciplina.AlunoDisciplinas)
                                .ThenInclude(alunoDisciplinas => alunoDisciplinas.Aluno);
                                
            }
            query = query.AsNoTracking()
                            .OrderBy(p => p.Id);
                return await query.ToListAsync();
        }

        public async Task<IEnumerable<Professor>> GetProfessoresByDisciplinaId(int id, bool includeAluno = false)
        {
            IQueryable<Professor> query = Context.Professores;

            if (includeAluno)
            {
                query = query.Include(professor => professor.Disciplinas)
                                .ThenInclude(disciplina => disciplina.AlunoDisciplinas)
                                .ThenInclude(alunoDisciplinas => alunoDisciplinas.Aluno);
            }
            query = query.AsNoTracking()
                            .OrderBy(professor => professor.Id)
                            .Where(professor => professor.Disciplinas.Any(
                                disciplina => disciplina.AlunoDisciplinas.Any(
                                    alunoDisciplinas => alunoDisciplinas.DisciplinaId == id)
                                    ));
                return await query.ToListAsync();
        }

        public async Task<IEnumerable<Disciplina>> GetDisciplinas()
        {
            var disciplinas = Context.Disciplinas.AsNoTracking()
                .OrderBy(d => d.Id);

            return await disciplinas.ToListAsync();
        }


        public async Task<Disciplina> GetDisciplina(int id)
        {
            var disciplina = await Context.Disciplinas.AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
                                                    

            return disciplina;
        }

        public async Task<IEnumerable<Curso>> GetCursos()
        {
            var cursos = await Context.Cursos.AsNoTracking()
                .ToListAsync();
            return cursos;
        }

        public async Task<Curso> GetCurso(int id)
        {
            var curso = await Context.Cursos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

                return curso;
        }
    }
}