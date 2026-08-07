namespace PruebaIdeasGroup.Application.Ports.In;

public interface IPdfReporteService
{
    Task<byte[]> GenerarReporteProyectoPdfAsync(int proyectoId);
}