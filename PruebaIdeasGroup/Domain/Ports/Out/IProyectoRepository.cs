namespace PruebaIdeasGroup.Domain.Ports.Out;

using PruebaIdeasGroup.Domain.Entities;

public interface IProyectoRepository
{
    Task<Proyecto?> GetByIdAsync(int id);
    Task<IEnumerable<Proyecto>> GetAllAsync();
    Task AddAsync(Proyecto proyecto);
    Task UpdateAsync(Proyecto proyecto);
    Task DeleteAsync(int id);
}