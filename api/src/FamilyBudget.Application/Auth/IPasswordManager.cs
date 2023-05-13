namespace FamilyBudget.Application.Auth;

public interface IPasswordManager
{
    string Secure(string password);
    bool Validate(string password, string securedPassword);
}