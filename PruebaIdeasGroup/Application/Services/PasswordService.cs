using BCrypt.Net;
using PruebaIdeasGroup.Application.Ports.In;

namespace PruebaIdeasGroup.Application.Services;

public class PasswordService : IPasswordService
{
    public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool Verify(string password, string passwordHash) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
}