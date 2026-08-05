namespace PruebaIdeasGroup.Application.Services;

using AutoMapper;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Domain.Entities;
using PruebaIdeasGroup.Domain.Ports.Out;

public class ColumnaService : IColumnaService
{
    private readonly IColumnaRepository _columnaRepository;
    private readonly IMapper _mapper;

    public ColumnaService(IColumnaRepository columnaRepository, IMapper mapper)
    {
        _columnaRepository = columnaRepository;
        _mapper = mapper;
    }

    public async Task<ColumnaDto?> GetByIdAsync(int id)
    {
        var columna = await _columnaRepository.GetByIdAsync(id);
        return _mapper.Map<ColumnaDto?>(columna);
    }

    public async Task<IEnumerable<ColumnaDto>> GetByProyectoIdAsync(int proyectoId)
    {
        var columnas = await _columnaRepository.GetByProyectoIdAsync(proyectoId);
        return _mapper.Map<IEnumerable<ColumnaDto>>(columnas);
    }

    public async Task<ColumnaDto> CreateAsync(CreateColumnaDto dto)
    {
        var columna = new Columna(dto.Nombre, dto.OrdenDentroProyecto, dto.ProyectoId);
        await _columnaRepository.AddAsync(columna);

        var columnaCreada = await _columnaRepository.GetByIdAsync(columna.Id);
        return _mapper.Map<ColumnaDto>(columnaCreada ?? columna);
    }

    public async Task UpdateAsync(int id, UpdateColumnaDto dto)
    {
        var columna = await _columnaRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"La columna con ID {id} no fue encontrada.");

        columna.Nombre = dto.Nombre;
        columna.OrdenDentroProyecto = dto.OrdenDentroProyecto;
        columna.Modificado = DateTime.UtcNow;

        await _columnaRepository.UpdateAsync(columna);
    }

    public async Task DeleteAsync(int id)
    {
        var columna = await _columnaRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"La columna con ID {id} no fue encontrada.");

        await _columnaRepository.DeleteAsync(id);
    }
}