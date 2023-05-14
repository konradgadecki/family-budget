using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.Commands;
using FamilyBudget.Application.Commands.Handlers;
using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Queries;
using FamilyBudget.Application.Queries.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudget.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<SignIn>, SignInHandler>();
        services.AddScoped<ICommandHandler<SignUp>, SignUpHandler>();
        services.AddScoped<ICommandHandler<CreateBudget>, CreateBudgetHandler>();
        services.AddScoped<IQueryHandler<FetchBudgets, IEnumerable<BudgetsDto>>, FetchBudgetsHandler>();
        services.AddScoped<IQueryHandler<FetchCategories, IEnumerable<CategoryDto>>, FetchCategoriesHandler>();

        return services;
    }
}