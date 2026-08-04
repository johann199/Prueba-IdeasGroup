using System.Runtime;
using System.Runtime.CompilerServices;
using AutoMapper;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Domain.Entities;

namespace PruebaIdeasGroup.Application.Mapping;

public class ProyectoProfile : Profile
{
    public ProyectoProfile()
    {
        CreateMap<Proyecto, ProyectoDto>();
    }
}