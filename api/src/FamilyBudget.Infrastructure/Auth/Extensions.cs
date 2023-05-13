using FamilyBudget.Application.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FamilyBudget.Infrastructure.Auth;

internal static class Extensions
{
    //TODO move constants to configuration file
    private const string ISSUER = "familybudget-issuer";
    private const string AUDIENCE = "familybudget-audience";
    private const string SIGNING_KEY = "3Laf5qgM43LiNgdZD1vma1eRHH5Xbd3RlsYmyIe-1es";
 
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>();

        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.Audience = AUDIENCE;
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = ISSUER,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SIGNING_KEY))
                };
            });

        return services;
    }
}