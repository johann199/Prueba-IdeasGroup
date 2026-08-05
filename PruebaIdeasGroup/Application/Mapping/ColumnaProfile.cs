namespace PruebaIdeasGroup.Application.Mappings;

using AutoMapper;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Domain.Entities;

public class ColumnaProfile : Profile
{
    public ColumnaProfile()
    {
        CreateMap<Columna, ColumnaDto>();
    }
}