namespace PruebaIdeasGroup.Application.Mappings;

using System.Linq;
using AutoMapper;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Domain.Entities;

public class TareaProfile : Profile
{
    public TareaProfile()
    {
        CreateMap<Tarea, TareaDto>()
            .ForMember(dest => dest.ResponsablesIds, 
                       opt => opt.MapFrom(src => src.Responsables.Select(r => r.UsuarioId)));
    }
}