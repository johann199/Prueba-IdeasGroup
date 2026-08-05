namespace PruebaIdeasGroup.Domain.Ports.Out;

using PruebaIdeasGroup.Domain.Entities;

public interface ITareaRepository
{
    Task<Tarea?> GetByIdAsync(int id);
    Task<IEnumerable<Tarea>> GetByColumnaIdAsync(int columnaId);
    Task AddAsync(Tarea tarea);
    Task UpdateAsync(Tarea tarea);
    Task DeleteAsync(int id);
}