using AutoMapper;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Domain.Entities;


namespace PruebaIdeasGroup.Application.Mapping;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<Usuario, UsuarioDto>();
        //CreateMap<CreatedUsuarioDto, Usuario>();
        //CreateMap<UpdatedUsuarioDto, Usuario>();
    }

}