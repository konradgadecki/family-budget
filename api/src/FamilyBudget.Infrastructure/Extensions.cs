using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using FamilyBudget.Infrastructure.Auth;
using Microsoft.AspNetCore.Builder;
using FamilyBudget.Core.Repositories;
using FamilyBudget.Infrastructure.DAL.Repository;

namespace FamilyBudget.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();

        services.AddSwaggerGen(swagger =>
        {
            swagger.EnableAnnotations();
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FamilyBudget API",
                Version = "v1"
            });
        });

        services.AddScoped<IUserRepository, InMemoryUserRepository>();
        services.AddAuth();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(reDoc =>
        {
            reDoc.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            reDoc.DocumentTitle = "FamilyBudget API";
        });
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}