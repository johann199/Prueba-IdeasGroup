namespace PruebaIdeasGroup.Application.Ports.In;

using PruebaIdeasGroup.Application.Dtos;

public interface ITareaService
{
    Task<TareaDto?> GetByIdAsync(int id);
    Task<IEnumerable<TareaDto>> GetByColumnaIdAsync(int columnaId);
    Task<TareaDto> CreateAsync(CreateTareaDto dto);
    Task UpdateAsync(int id, UpdateTareaDto dto);
    Task AddResponsableAsync(int tareaId, int usuarioId);
    Task DeleteAsync(int id);
}