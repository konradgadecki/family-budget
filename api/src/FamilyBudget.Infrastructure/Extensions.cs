using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using FamilyBudget.Application.Auth;
using FamilyBudget.Core.Abstractions;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;
using FamilyBudget.Infrastructure.Auth;
using FamilyBudget.Infrastructure.DAL.Repository;

namespace FamilyBudget.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowMyOrigin",
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000") // your client's origin
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
        });
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


        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddSingleton<IPasswordManager, PasswordManager>();
        services.AddSingleton<IClock, Clock>();
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        services.AddAuth();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseCors("AllowMyOrigin");

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