using PruebaIdeasGroup.Application.Dtos;

namespace PruebaIdeasGroup.Domain.Ports;

public interface IUsuarioService
{
    Task<UsuarioDto?> GetByIdAsync(int id);
    Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto);
    Task<IEnumerable<UsuarioDto>> GetAllAsync();
    Task<bool> UpdateAsync(int id, UpdateUsuarioDto dto);
    Task<bool> DeleteAsync(int id);
}