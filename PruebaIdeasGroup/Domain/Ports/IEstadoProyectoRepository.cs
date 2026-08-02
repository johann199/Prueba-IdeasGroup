using PruebaIdeasGroup.Domain.Entities;
namespace PruebaIdeasGroup.Domain.Ports;


public interface IEstadoProyectoRepository
{
    Task<EstadoProyecto?> GetByIdAsync(int id);
    Task<IEnumerable<EstadoProyecto>> GetAllAsync();
    Task AddAsync(EstadoProyecto estadoProyecto);
    Task UpdateAsync(EstadoProyecto estadoProyecto);
    Task DeleteAsync(int id);
}