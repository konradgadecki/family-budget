using Microsoft.AspNetCore.Identity;
using FamilyBudget.Core.Entities;
using FamilyBudget.Application.Auth;

namespace FamilyBudget.Infrastructure.Auth;

internal sealed class PasswordManager : IPasswordManager
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordManager(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public string Secure(string password) => _passwordHasher.HashPassword(default, password);

    public bool Validate(string password, string securedPassword)
    {
        var verificationResult = _passwordHasher.VerifyHashedPassword(default, securedPassword, password);
        
        return verificationResult == PasswordVerificationResult.Success;
    }
}