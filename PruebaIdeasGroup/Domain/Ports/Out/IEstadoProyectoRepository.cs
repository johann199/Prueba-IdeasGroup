namespace PruebaIdeasGroup.Domain.Ports.Out;
using PruebaIdeasGroup.Domain.Entities;

public interface IEstadoProyectoRepository
{
    Task<EstadoProyecto?> GetByIdAsync(int id);
    Task<IEnumerable<EstadoProyecto>> GetAllAsync();
    Task AddAsync(EstadoProyecto estadoProyecto);
    Task UpdateAsync(EstadoProyecto estadoProyecto);
    Task DeleteAsync(int id);
}