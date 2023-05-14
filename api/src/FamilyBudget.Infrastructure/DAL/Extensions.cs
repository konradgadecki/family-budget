using FamilyBudget.Core.Repositories;
using FamilyBudget.Infrastructure.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyBudget.Infrastructure.DAL;


internal static class Extensions
{
    private const string PostgresConnString = "Server=host.docker.internal;Database=FamilyBudget;Username=postgres;Password=";

    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {    
        services.AddDbContext<FamilyBudgetDbContext>(x => x.UseNpgsql(PostgresConnString));
        services.AddScoped<IUserRepository, DbUserRepository>();
        services.AddScoped<IBudgetRepository, DbBudgetRepository>();
        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
}