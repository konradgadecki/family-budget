using FamilyBudget.Application.DTO;

namespace  FamilyBudget.Application.Auth;

public interface IAuthenticator
{
    JwtDto CreateToken(Guid userId, string role);
}