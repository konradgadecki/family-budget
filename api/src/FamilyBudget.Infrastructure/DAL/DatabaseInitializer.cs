using FamilyBudget.Core.Abstractions;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 

namespace FamilyBudget.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    // the service locator anti-pattern used exceptionally
    private readonly IServiceProvider _serviceProvider;
    private readonly IClock _clock;

    public DatabaseInitializer(IServiceProvider serviceProvider, IClock clock)
    {
        _serviceProvider = serviceProvider;
        _clock = clock;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<FamilyBudgetDbContext>();

        var migrations = await dbContext.Database.GetAppliedMigrationsAsync(cancellationToken);

        if (!migrations.Any(x => x.Contains("Init")))
        {
            await dbContext.Database.MigrateAsync(cancellationToken);
            await Initialize(dbContext, cancellationToken);
        }
    }

    private async Task Initialize(FamilyBudgetDbContext dbContext, CancellationToken cancellationToken)
    {
        const string PASSWORD = "AQAAAAIAAYagAAAAENLG7z25ckkVhX58acf0u5UkUacf2xdp4YfBoi1uoSGQ3f9raKXYFalMdocIAkhxRA=="; //password
        const string KOGA_PASS = "AQAAAAIAAYagAAAAED7fGHRSw/TWJETt3oh3wEOyo5oHUVHmZEm/s3AFxA7QkA8Y/BQjCIdjgnLTVogm2w==";

        if (!await dbContext.Categories.AnyAsync(cancellationToken))
        {
            var categories = new List<Category>()
            {
                new Category(1, "Selfcare"),
                new Category(2, "Entertainment"),
                new Category(3, "Education"),
                new Category(4, "Transportation"),
                new Category(5, "Groceries"),
                new Category(6, "Travel"),
            };

            await dbContext.Categories.AddRangeAsync(categories, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        var users = new List<User>()
        {
            new User(Guid.Parse("77777777-7777-7777-7777-777777777777"), "koga@gmail.com", KOGA_PASS, "user", _clock.Current()),
            new User(Guid.NewGuid(), "karol@gmail.com", PASSWORD, "user", _clock.Current()),
            new User(Guid.NewGuid(), "marek@gmail.com", PASSWORD, "user", _clock.Current()),
            new User(Guid.NewGuid(), "admin@gmail.com", PASSWORD, "admin", _clock.Current())
        };

        var useeee = await dbContext.Users.ToListAsync(cancellationToken);
        if (!await dbContext.Users.AnyAsync(cancellationToken))
        {
            await dbContext.Users.AddRangeAsync(users, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        if (!await dbContext.Budgets.AnyAsync(cancellationToken))
        {
            var random = new Random();

            var karolId = users.Single(x => x.Email == "karol@gmail.com").Id;
            var karolBudgets = new List<Budget>()
            {
                new Budget(Guid.NewGuid(), karolId, random.Next(1, 6), "April", 12000, 10000, false),
                new Budget(Guid.NewGuid(), karolId, random.Next(1, 6), "June", 25000, 28500, true),
                new Budget(Guid.NewGuid(), karolId, random.Next(1, 6), "September", 32000, 34000, false),
                new Budget(Guid.NewGuid(), karolId, random.Next(1, 6), "July", 22000, 20000, false),
                new Budget(Guid.NewGuid(), karolId, random.Next(1, 6), "May", 18000, 16000, true),
                new Budget(Guid.NewGuid(), karolId, random.Next(1, 6), "December", 28000, 26000, true),
                new Budget(Guid.NewGuid(), karolId, random.Next(1, 6), "October", 35000, 32000, false),
                new Budget(Guid.NewGuid(), karolId, random.Next(1, 6), "August", 16000, 14000, true),
                new Budget(Guid.NewGuid(), karolId, random.Next(1, 6), "November", 22000, 25600, false),
                new Budget(Guid.NewGuid(), karolId, random.Next(1, 6), "January", 20000, 18000, true)
            };

            var konradId = users.Single(x => x.Email == "koga@gmail.com").Id;
            var konradBudgets = new List<Budget>()
            {
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "April", 15000, 14000, false),
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "April", 15000, 14000, false),
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "May", 20000, 18000, true),
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "June", 30000, 35000, false),
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "August", 18000, 16600, false),
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "November", 25000, 27800, true),
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "October", 40000, 38000, true),
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "July", 25000, 20000, true),
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "September", 35000, 32000, true),
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "December", 30000, 28000, true),
                new Budget(Guid.NewGuid(), konradId, random.Next(1, 6), "January", 22000, 20000, true)
            };

            var marekId = users.Single(x => x.Email == "marek@gmail.com").Id;
            var marekBudgets = new List<Budget>()
            {
                new Budget(Guid.NewGuid(), marekId, random.Next(1, 6), "April", 14000, 12000, false),
                new Budget(Guid.NewGuid(), marekId, random.Next(1, 6), "June", 28000, 29010, false),
                new Budget(Guid.NewGuid(), marekId, random.Next(1, 6), "August", 20000, 55000, false),
                new Budget(Guid.NewGuid(), marekId, random.Next(1, 6), "May", 22000, 20000, true),
                new Budget(Guid.NewGuid(), marekId, random.Next(1, 6), "October", 42000, 40000, false),
                new Budget(Guid.NewGuid(), marekId, random.Next(1, 6), "July", 26000, 24500, true),
                new Budget(Guid.NewGuid(), marekId, random.Next(1, 6), "December", 32000, 30000, false),
                new Budget(Guid.NewGuid(), marekId, random.Next(1, 6), "September", 38000, 45500, true),
                new Budget(Guid.NewGuid(), marekId, random.Next(1, 6), "January", 24000, 31000, false),
                new Budget(Guid.NewGuid(), marekId, random.Next(1, 6), "November", 28000, 26000, true)
            };

            await dbContext.Budgets.AddRangeAsync(karolBudgets, cancellationToken);
            await dbContext.Budgets.AddRangeAsync(konradBudgets, cancellationToken);
            await dbContext.Budgets.AddRangeAsync(marekBudgets, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}