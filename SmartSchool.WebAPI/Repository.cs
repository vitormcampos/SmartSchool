using System.Collections.Generic;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI
{
    public static class Repository
    {
        public static List<Aluno> Alunos = new List<Aluno>
        {
                new Aluno() {
                    Id = 1,
                    Nome = "Vitor",
                    Sobrenome = "Campos",
                    Telefone = "11930855887"
                },
                new Aluno() {
                    Id = 2,
                    Nome = "Micaely",
                    Sobrenome = "Silva",
                    Telefone = "11930855887"
                },
                new Aluno() {
                    Id = 3,
                    Nome = "Fernanda",
                    Sobrenome = "Campos",
                    Telefone = "11930855887"
                },

            };

        public static List<Professor> Professores = new List<Professor> 
        { 
            new Professor() {
                Id = 1,
                Nome = "Dada"
            },
            new Professor() {
                Id = 2,
                Nome = "Livia"
            },
            new Professor() {
                Id = 3,
                Nome = "Juliana"
            },
            new Professor() {
                Id = 4,
                Nome = "Claudia"
            }
         };

        public static List<Disciplina> Disciplinas = new List<Disciplina> 
        { 
            new Disciplina() {
                Id = 1,
                Nome = "Programação Web",
                Professor = Professores[1],
                ProfessorId = 1
            },
            new Disciplina() {
                Id = 2,
                Nome = "TCC",
                Professor = Professores[2],
                ProfessorId = 2
            },
            new Disciplina() {
                Id = 3,
                Nome = "Programação Mobile",
                Professor = Professores[1],
                ProfessorId = 1
            },
            new Disciplina() {
                Id = 4,
                Nome = "Banco de dados",
                Professor = Professores[3],
                ProfessorId = 3
            },


         };
         
    }
}