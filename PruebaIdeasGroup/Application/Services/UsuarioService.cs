namespace PruebaIdeasGroup.Application.Services;

using AutoMapper;
using PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Domain.Ports.Out;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Domain.Entities;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordHasher;

    public UsuarioService(IUsuarioRepository repository, IMapper mapper, IPasswordService passwordHasher)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto)
    {
        var contrasenaHash = _passwordHasher.Hash(dto.Contrasena);
        var usuario = new Usuario(dto.Nombre, dto.Correo, contrasenaHash);
        await _repository.AddAsync(usuario);
        return _mapper.Map<UsuarioDto>(usuario);
    }

    public async Task<UsuarioDto?> GetByIdAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        return usuario is null ? null : _mapper.Map<UsuarioDto>(usuario);
    }

    public async Task<UsuarioDto?> GetByCorreoAsync(string correo)
    {
        var usuario = await _repository.GetByCorreoAsync(correo);
        return usuario is null ? null : _mapper.Map<UsuarioDto>(usuario);
    }

    public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
    {
        var usuarios = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
    }

    public async Task<bool> UpdateAsync(int id, UpdateUsuarioDto dto)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is null)
            return false;

        usuario.Nombre = dto.Nombre;
        usuario.Correo = dto.Correo;
        usuario.Modificado = DateTime.UtcNow;

        await _repository.UpdateAsync(usuario);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario is null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}