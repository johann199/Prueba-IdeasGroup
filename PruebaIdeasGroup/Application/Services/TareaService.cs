namespace PruebaIdeasGroup.Application.Services;

using AutoMapper;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Domain.Entities;
using PruebaIdeasGroup.Domain.Ports.Out;

public class TareaService : ITareaService
{
    private readonly ITareaRepository _tareaRepository;
    private readonly IMapper _mapper;

    public TareaService(ITareaRepository tareaRepository, IMapper mapper)
    {
        _tareaRepository = tareaRepository;
        _mapper = mapper;
    }

    public async Task<TareaDto?> GetByIdAsync(int id)
    {
        var tarea = await _tareaRepository.GetByIdAsync(id);
        return _mapper.Map<TareaDto?>(tarea);
    }

    public async Task<IEnumerable<TareaDto>> GetByColumnaIdAsync(int columnaId)
    {
        var tareas = await _tareaRepository.GetByColumnaIdAsync(columnaId);
        return _mapper.Map<IEnumerable<TareaDto>>(tareas);
    }

    public async Task<TareaDto> CreateAsync(CreateTareaDto dto)
    {
        var tarea = new Tarea(
            dto.Nombre,
            dto.Descripcion,
            dto.Prioridad,
            dto.OrdenDentroColumna,
            dto.ColumnaId
        );

        await _tareaRepository.AddAsync(tarea);

        var tareaCreada = await _tareaRepository.GetByIdAsync(tarea.Id);
        return _mapper.Map<TareaDto>(tareaCreada ?? tarea);
    }

    public async Task UpdateAsync(int id, UpdateTareaDto dto)
    {
        var tarea = await _tareaRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"La tarea con ID {id} no fue encontrada.");

        tarea.Nombre = dto.Nombre;
        tarea.Descripcion = dto.Descripcion;
        tarea.Prioridad = dto.Prioridad;
        tarea.OrdenDentroColumna = dto.OrdenDentroColumna;
        tarea.ColumnaId = dto.ColumnaId;
        tarea.Modificado = DateTime.UtcNow;

        await _tareaRepository.UpdateAsync(tarea);
    }

    public async Task AddResponsableAsync(int tareaId, int usuarioId)
    {
        var tarea = await _tareaRepository.GetByIdAsync(tareaId)
            ?? throw new KeyNotFoundException($"La tarea con ID {tareaId} no fue encontrada.");
        tarea.AddResponsableTarea(usuarioId);

        await _tareaRepository.UpdateAsync(tarea);
    }

    public async Task DeleteAsync(int id)
    {
        var tarea = await _tareaRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"La tarea con ID {id} no fue encontrada.");

        await _tareaRepository.DeleteAsync(id);
    }
}