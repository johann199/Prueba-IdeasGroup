namespace PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Application.Dtos;

public interface IEstadoProyectoService
{
    Task<EstadoProyectoDto?> GetByIdAsync(int id);
    Task <EstadoProyectoDto> CreateAsync(CreateEstadoProyectoDto dto);
    Task <IEnumerable<EstadoProyectoDto>> GetAllAsync();
    Task <bool>UpdateAsync(int id, UpdateEstadoProyectoDto dto);
    Task <bool> DeleteAsync(int id);
}