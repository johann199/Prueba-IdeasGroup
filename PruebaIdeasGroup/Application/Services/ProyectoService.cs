namespace PruebaIdeasGroup.Application.Services;

using AutoMapper;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Domain.Entities;
using PruebaIdeasGroup.Domain.Ports.Out;

public class ProyectoService : IProyectoService
{
    private readonly IProyectoRepository _proyectoRepository;
    private readonly IMapper _mapper;

    public ProyectoService(IProyectoRepository proyectoRepository, IMapper mapper)
    {
        _proyectoRepository = proyectoRepository;
        _mapper = mapper;
    }

    public async Task<ProyectoDto?> GetByIdAsync(int id)
    {
        var proyecto = await _proyectoRepository.GetByIdAsync(id);
        return _mapper.Map<ProyectoDto?>(proyecto);
    }

    public async Task<IEnumerable<ProyectoDto>> GetAllAsync()
    {
        var proyectos = await _proyectoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProyectoDto>>(proyectos);
    }

    public async Task<ProyectoDto> CreateAsync(CreateProyectoDto dto)
    {
        var proyecto = new Proyecto(
            dto.Nombre,
            dto.Descripcion,
            dto.FechaInicio,
            dto.FechaFin,
            dto.CreadoPorId,
            dto.EstadoId
        );

        await _proyectoRepository.AddAsync(proyecto);

        var proyectoCreado = await _proyectoRepository.GetByIdAsync(proyecto.Id);
        return _mapper.Map<ProyectoDto>(proyectoCreado ?? proyecto);
    }

    public async Task UpdateAsync(int id, UpdateProyectoDto dto)
    {
        var proyecto = await _proyectoRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"El proyecto con ID {id} no fue encontrado.");

        proyecto.Nombre = dto.Nombre;
        proyecto.Descripcion = dto.Descripcion;
        proyecto.FechaInicio = dto.FechaInicio;
        proyecto.FechaFin = dto.FechaFin;
        proyecto.EstadoId = dto.EstadoId;
        proyecto.Modificado = DateTime.UtcNow;

        await _proyectoRepository.UpdateAsync(proyecto);
    }

    public async Task DeleteAsync(int id)
    {
        var proyecto = await _proyectoRepository.GetByIdAsync(id);
        if (proyecto == null)
        {
            throw new KeyNotFoundException($"El proyecto con ID {id} no fue encontrado.");
        }

        await _proyectoRepository.DeleteAsync(id);
    }
}