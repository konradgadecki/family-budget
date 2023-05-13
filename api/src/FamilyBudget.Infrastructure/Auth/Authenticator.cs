using FamilyBudget.Application.Auth;
using FamilyBudget.Application.DTO;
using FamilyBudget.Core.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FamilyBudget.Infrastructure.Auth;

internal sealed class Authenticator : IAuthenticator
{
    //TODO move constants to configuration file
    private const string ISSUER = "familybudget-issuer";
    private const string AUDIENCE = "familybudget-audience";
    private const string SIGNING_KEY = "3Laf5qgM43LiNgdZD1vma1eRHH5Xbd3RlsYmyIe-1es";
    private const string EXPIRY = "00.01:00:00";

    private readonly IClock _clock;
    private readonly string _issuer;
    private readonly TimeSpan _expiry;
    private readonly string _audience;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new JwtSecurityTokenHandler();

    public Authenticator(IClock clock)
    {
        _clock = clock;
        _issuer = ISSUER;
        _audience = AUDIENCE;
        _expiry = TimeSpan.Parse(EXPIRY);
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(SIGNING_KEY)),
                SecurityAlgorithms.HmacSha256);
    }

    public JwtDto CreateToken(Guid userId, string role)
    {
        var now = _clock.Current();
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(ClaimTypes.Role, role)
        };

        var expires = now.Add(_expiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return new JwtDto
        {
            AccessToken = token
        };
    }
}