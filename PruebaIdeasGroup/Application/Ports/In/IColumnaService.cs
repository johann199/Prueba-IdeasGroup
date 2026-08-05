namespace PruebaIdeasGroup.Application.Ports.In;

using PruebaIdeasGroup.Application.Dtos;

public interface IColumnaService
{
    Task<ColumnaDto?> GetByIdAsync(int id);
    Task<IEnumerable<ColumnaDto>> GetByProyectoIdAsync(int proyectoId);
    Task<ColumnaDto> CreateAsync(CreateColumnaDto dto);
    Task UpdateAsync(int id, UpdateColumnaDto dto);
    Task DeleteAsync(int id);
}