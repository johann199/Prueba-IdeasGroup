namespace PruebaIdeasGroup.Domain.Ports.Out;

using PruebaIdeasGroup.Domain.Entities;

public interface IColumnaRepository
{
    Task<Columna?> GetByIdAsync(int id);
    Task<IEnumerable<Columna>> GetByProyectoIdAsync(int proyectoId);
    Task AddAsync(Columna columna);
    Task UpdateAsync(Columna columna);
    Task DeleteAsync(int id);
}