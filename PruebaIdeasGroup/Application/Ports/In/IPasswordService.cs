namespace PruebaIdeasGroup.Application.Ports.In;

public interface IPasswordService
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}