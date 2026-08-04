namespace PruebaIdeasGroup.Application.Services;
using AutoMapper;
using PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Domain.Ports.Out;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Domain.Entities;

public class EstadoProyectoService : IEstadoProyectoService
{
    private readonly IEstadoProyectoRepository _repository;
    private readonly IMapper _mapper;

    public EstadoProyectoService(IEstadoProyectoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<EstadoProyectoDto> CreateAsync(CreateEstadoProyectoDto dto)
    {
        var estadoProyecto = new EstadoProyecto(dto.Nombre);
        await _repository.AddAsync(estadoProyecto);
        return _mapper.Map<EstadoProyectoDto>(estadoProyecto);
    }

    public async Task<EstadoProyectoDto?> GetByIdAsync(int id)
    {
        var estadoProyecto = await _repository.GetByIdAsync(id);
        return estadoProyecto is null ? null : _mapper.Map<EstadoProyectoDto>(estadoProyecto);
    }

    public async Task<IEnumerable<EstadoProyectoDto>> GetAllAsync()
    {
        var estadosProyecto = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<EstadoProyectoDto>>(estadosProyecto);
    }

    public async Task<bool> UpdateAsync(int id, UpdateEstadoProyectoDto dto)
    {
        var estadoProyecto = await _repository.GetByIdAsync(id);
        if (estadoProyecto is null)
            return false;
        
        estadoProyecto.Nombre = dto.Nombre;
        estadoProyecto.Modificado = DateTime.UtcNow;
        await _repository.UpdateAsync(estadoProyecto);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var estadoProyecto = await _repository.GetByIdAsync(id);
        if (estadoProyecto is null)
            return false;
        
        await _repository.DeleteAsync(id);
        return true;
    }
}