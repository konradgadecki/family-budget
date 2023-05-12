using FamilyBudget.Application.Auth;
using FamilyBudget.Application.DTO;

namespace FamilyBudget.Infrastructure.Auth;

internal sealed class Authenticator : IAuthenticator
{    
    public JwtDto CreateToken(Guid userId, string role)
    {
        // TODO implement real token creation

        return new JwtDto
        {
            AccessToken = "token"
        };
    }
}