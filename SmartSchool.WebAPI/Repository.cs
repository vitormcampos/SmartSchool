using System.Collections.Generic;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI
{
    public static class Repository
    {
        public static IEnumerable<Aluno> Alunos()
        {
            var lista = new List<Aluno>() {
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
            return lista;
        }
    }
}