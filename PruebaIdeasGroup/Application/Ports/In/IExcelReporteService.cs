namespace PruebaIdeasGroup.Application.Ports.In;

public interface IExcelReporteService
{
    Task<byte[]> GenerarReporteProyectoExcelAsync(int proyectoId);
}