namespace PruebaIdeasGroup.Application.Dtos;

public record LoginDto(string Correo, string Password);
public record AuthResultDto(string Token, int UserId, string Nombre, string Correo);