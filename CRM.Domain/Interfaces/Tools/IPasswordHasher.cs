namespace CRM.Domain.Interfaces.Tools;

public interface IPasswordHasher
{
    string Generate(string password);

    bool Verify(string password, string hashedPassword);
}
