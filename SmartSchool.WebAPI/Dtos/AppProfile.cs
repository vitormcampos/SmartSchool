using System;
using AutoMapper;
using SmartSchool.WebAPI.Helpers;
using SmartSchool.WebAPI.Models;

namespace SmartSchool.WebAPI.Dtos
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {

            ///Mapeamento da entidade Aluno
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(
                        src => $"{src.Nome} {src.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(
                        src => src.DataNascimento.GetCurrentAge())
                );
            CreateMap<AlunoDto, Aluno>();


            ///Mapeamento da entidade Professor
            CreateMap<Professor, ProfessorDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(
                        src => $"{src.Nome} {src.Sobrenome}"));
            CreateMap<ProfessorDto, Professor>();


            ///Mapeamento da entidade Disciplina
            CreateMap<Disciplina, Disciplina>().ReverseMap();

            ///Mapeamento da entidade Curso
            CreateMap<Curso, CursoDto>().ReverseMap();
        }
    }
}