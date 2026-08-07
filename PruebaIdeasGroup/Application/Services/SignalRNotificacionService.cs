namespace PruebaIdeasGroup.Application.Services;

using Microsoft.AspNetCore.SignalR;
using PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Infrastructure.Hubs;

public class SignalRNotificacionService : INotificacionService
{
    private readonly IHubContext<BoardHub> _hubContext;

    public SignalRNotificacionService(IHubContext<BoardHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotificarMovimientoTareaAsync(int proyectoId, int tareaId, int columnaId, int nuevoOrden)
    {
        await _hubContext.Clients.Group($"Project_{proyectoId}")
            .SendAsync("TaskMoved", new { TareaId = tareaId, ColumnaId = columnaId, NuevoOrden = nuevoOrden });
    }

    public async Task NotificarActualizacionTableroAsync(int proyectoId)
    {
        await _hubContext.Clients.Group($"Project_{proyectoId}")
            .SendAsync("BoardUpdated");
    }
}