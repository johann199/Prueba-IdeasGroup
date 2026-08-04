namespace PruebaIdeasGroup.Application.Ports.In;

using PruebaIdeasGroup.Application.Dtos;

public interface IProyectoService
{
    Task<ProyectoDto?> GetByIdAsync(int id);
    Task<IEnumerable<ProyectoDto>> GetAllAsync();
    Task<ProyectoDto> CreateAsync(CreateProyectoDto dto);
    Task UpdateAsync(int id, UpdateProyectoDto dto);
    Task DeleteAsync(int id);
}