using PruebaIdeasGroup.Application.Dtos;

namespace PruebaIdeasGroup.Application.Ports.In;

public interface IAuthService
{
    Task<AuthResultDto> LoginAsync(LoginDto dto);
}