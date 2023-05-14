using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;
using FamilyBudget.Core.ValueObjects;
using System;
using System.Collections.Immutable;

namespace FamilyBudget.Infrastructure.DAL.Repository;

internal class InMemoryBudgetRepository : IBudgetRepository
{
    private readonly List<Budget> _budgets;
    private readonly List<Category> _categories;
    private readonly IUserRepository _userRepository;

    public InMemoryBudgetRepository(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        _categories = new List<Category>()
        {
            new Category(1, "Selfcare"),
            new Category(2, "Entertainment"),
            new Category(3, "Education"),
            new Category(4, "Transportation"),
            new Category(5, "Groceries"),
            new Category(6, "Travel"),
        };

        var random = new Random();

        var karolId = _userRepository.Users.Single(x => x.Email == "karol@gmail.com").Id;
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

        var konradId = _userRepository.Users.Single(x => x.Email == "koga@gmail.com").Id;
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

        var marekId = _userRepository.Users.Single(x => x.Email == "marek@gmail.com").Id;
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

        _budgets = new List<Budget>();
        _budgets.AddRange(karolBudgets);
        _budgets.AddRange(konradBudgets);
        _budgets.AddRange(marekBudgets);

    }

    public async Task<IImmutableList<Budget>> FetchBudgetsAsync(UserId userId)
    {
        await Task.CompletedTask;

        var myBudgets = _budgets.Where(b => b.UserId == userId);
        var othersBudgets = _budgets.Where(b => b.UserId != userId && b.Shared);

        return myBudgets.Concat(othersBudgets).ToImmutableArray(); 
    }

    public async Task AddAsync(Budget newBudget, UserId userId)
    {
        await Task.CompletedTask;
    
        _budgets.Add(newBudget);
    }

    public async Task<Category> GetCategoryById(int categoryId)
    {
        await Task.CompletedTask;

        return _categories.FirstOrDefault(x => x.Id == categoryId);
    }

    public async Task<IImmutableList<Category>> GetAllCategoriesAsync()
    {
        await Task.CompletedTask;

        return _categories.ToImmutableList();
    }
}
