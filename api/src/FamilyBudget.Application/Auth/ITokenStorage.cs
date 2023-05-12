using FamilyBudget.Application.DTO;

namespace FamilyBudget.Application.Auth;

public interface ITokenStorage
{
    void Set(JwtDto jwt);
    JwtDto Get();
}