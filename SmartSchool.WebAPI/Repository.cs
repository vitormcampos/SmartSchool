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
                Nome = "Rogerio"
            },
            new Professor() {
                Id = 2,
                Nome = "Marcos"
            },
            new Professor() {
                Id = 3,
                Nome = "Juliana"
            },
            new Professor() {
                Id = 4,
                Nome = "Claudia"
            },
         };
    }
}