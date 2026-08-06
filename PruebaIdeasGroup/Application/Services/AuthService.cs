using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PruebaIdeasGroup.Application.Dtos;
using PruebaIdeasGroup.Application.Ports.In;
using PruebaIdeasGroup.Domain.Ports.Out;

namespace PruebaIdeasGroup.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordService _passwordHasher;
    private readonly IConfiguration _config;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        IPasswordService passwordHasher,
        IConfiguration config)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _config = config;
    }

    public async Task<AuthResultDto> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.GetByCorreoAsync(dto.Correo.ToLower().Trim());
        if (usuario == null)
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        bool esPasswordValida = _passwordHasher.Verify(dto.Password, usuario.Contrasena);
        if (!esPasswordValida)
            throw new UnauthorizedAccessException("Credenciales inválidas.");

        var token = GenerarTokenJwt(usuario.Id, usuario.Nombre, usuario.Correo);
        return new AuthResultDto(token, usuario.Id, usuario.Nombre, usuario.Correo);
    }

    private string GenerarTokenJwt(int userId, string nombre, string correo)
    {
        var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? _config["Jwt:Key"];
        var key = Encoding.UTF8.GetBytes(jwtKey!);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, nombre),
            new Claim(ClaimTypes.Email, correo)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(480),
            Issuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? _config["Jwt:Issuer"],
            Audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? _config["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}