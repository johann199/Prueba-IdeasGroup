namespace PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Application.Dtos;

public interface IUsuarioService
{
    Task<UsuarioDto?> GetByIdAsync(int id);
    Task<UsuarioDto?> GetByCorreoAsync(string correo);
    Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto);
    Task<IEnumerable<UsuarioDto>> GetAllAsync();
    Task<bool> UpdateAsync(int id, UpdateUsuarioDto dto);
    Task<bool> DeleteAsync(int id);
}