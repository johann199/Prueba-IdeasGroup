using AutoMapper;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Domain.Entities;

namespace PruebaIdeasGroup.Application.Mapping;

public class EstadoProyectoProfile : Profile
{
    public EstadoProyectoProfile()
    {
        CreateMap<EstadoProyecto, EstadoProyectoDto>();
        //CreateMap<CreatedEstadoProyectoDto, EstadoProyecto>();
        //CreateMap<UpdatedEstadoProyectoDto, EstadoProyecto>();
    }

}