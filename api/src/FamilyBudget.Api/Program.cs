using FamilyBudget.Application;
using FamilyBudget.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplication().AddInfrastructure();

var app = builder.Build();

app.UseInfrastructure();

app.UseAuthorization();

app.MapControllers();

app.Run();
