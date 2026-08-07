namespace PruebaIdeasGroup.Application.Ports.In;

public interface INotificacionService
{
    Task NotificarMovimientoTareaAsync(int proyectoId, int tareaId, int columnaId, int nuevoOrden);
    Task NotificarActualizacionTableroAsync(int proyectoId);
}