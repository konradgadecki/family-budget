using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.Commands;
using FamilyBudget.Application.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudget.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<SignIn>, SignInHandler>();

        return services;
    }
}