namespace PruebaIdeasGroup.Domain.Ports.Out;
using PruebaIdeasGroup.Domain.Entities;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByCorreoAsync(string correo);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task DeleteAsync(int id);
}