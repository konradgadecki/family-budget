using FamilyBudget.Application.Auth;
using FamilyBudget.Infrastructure.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudget.Infrastructure.Auth;

internal static class Extensions
{    
    public static IServiceCollection AddAuth(this IServiceCollection services)
    {
        services
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddSingleton<ITokenStorage, HttpContextTokenStorage>();

        return services;
    }
}