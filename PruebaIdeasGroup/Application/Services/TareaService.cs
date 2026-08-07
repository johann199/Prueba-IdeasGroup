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
    private readonly INotificacionService _notificacionService;

    public TareaService(
        ITareaRepository tareaRepository, 
        IMapper mapper, 
        INotificacionService notificacionService)
    {
        _tareaRepository = tareaRepository;
        _mapper = mapper;
        _notificacionService = notificacionService;
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

        var tareaCreada = await _tareaRepository.GetByIdAsync(tarea.Id) ?? tarea;

        
        if (tareaCreada.Columna != null)
        {
            await _notificacionService.NotificarActualizacionTableroAsync(tareaCreada.Columna.ProyectoId);
        }

        return _mapper.Map<TareaDto>(tareaCreada);
    }

    public async Task UpdateAsync(int id, UpdateTareaDto dto)
    {
        var tarea = await _tareaRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"La tarea con ID {id} no fue encontrada.");

        bool huboCambioDePosicion = tarea.ColumnaId != dto.ColumnaId || tarea.OrdenDentroColumna != dto.OrdenDentroColumna;

        tarea.Nombre = dto.Nombre;
        tarea.Descripcion = dto.Descripcion;
        tarea.Prioridad = dto.Prioridad;
        tarea.OrdenDentroColumna = dto.OrdenDentroColumna;
        tarea.ColumnaId = dto.ColumnaId;
        tarea.Modificado = DateTime.UtcNow;

        await _tareaRepository.UpdateAsync(tarea);

        if (huboCambioDePosicion && tarea.Columna != null)
        {
            await _notificacionService.NotificarMovimientoTareaAsync(
                tarea.Columna.ProyectoId, 
                tarea.Id, 
                tarea.ColumnaId, 
                tarea.OrdenDentroColumna
            );
        }
    }

    public async Task AddResponsableAsync(int tareaId, int usuarioId)
    {
        var tarea = await _tareaRepository.GetByIdAsync(tareaId)
            ?? throw new KeyNotFoundException($"La tarea con ID {tareaId} no fue encontrada.");
        
        tarea.AddResponsableTarea(usuarioId);

        await _tareaRepository.UpdateAsync(tarea);

        if (tarea.Columna != null)
        {
            await _notificacionService.NotificarActualizacionTableroAsync(tarea.Columna.ProyectoId);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var tarea = await _tareaRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"La tarea con ID {id} no fue encontrada.");

        int? proyectoId = tarea.Columna?.ProyectoId;

        await _tareaRepository.DeleteAsync(id);

        if (proyectoId.HasValue)
        {
            await _notificacionService.NotificarActualizacionTableroAsync(proyectoId.Value);
        }
    }
}