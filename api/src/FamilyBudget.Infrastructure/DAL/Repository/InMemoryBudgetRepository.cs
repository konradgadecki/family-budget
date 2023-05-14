using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;
using FamilyBudget.Core.ValueObjects;

namespace FamilyBudget.Infrastructure.DAL.Repository;

internal class InMemoryBudgetRepository : IBudgetRepository
{
    private readonly Dictionary<User, IEnumerable<Budget>> _budgets;

    public InMemoryBudgetRepository()
    {
        var karol = new User(Guid.NewGuid(), "karol@gmail.com", "password", "user", DateTime.Now);
        var karolBudgets = new List<Budget>()
        {
            new Budget("April", "Selfcare", 12000, 10000, false),
            new Budget("June", "Entertainment", 25000, 28500, true),
            new Budget("September", "Education", 32000, 34000, false),
            new Budget("July", "Transportation", 22000, 20000, false),
            new Budget("May", "Groceries", 18000, 16000, true),
            new Budget("December", "Gifts", 28000, 26000, true),
            new Budget("October", "Travel", 35000, 32000, false),
            new Budget("August", "Utilities", 16000, 14000, true),
            new Budget("November", "Dining Out", 22000, 25600, false),
            new Budget("January", "Healthcare", 20000, 18000, true)
        };

        var konrad = new User(Guid.Parse("77777777-7777-7777-7777-777777777777"), "koga@gmail.com", "password", "user", DateTime.Now);
        var konradBudgets = new List<Budget>()
        {
            new Budget("April", "Selfcare", 15000, 14000, false),
            new Budget("May", "Groceries", 20000, 18000, true),
            new Budget("June", "Entertainment", 30000, 35000, false),
            new Budget("August", "Utilities", 18000, 16600, false),
            new Budget("November", "Dining Out", 25000, 27800, true),
            new Budget("October", "Travel", 40000, 38000, true),
            new Budget("July", "Transportation", 25000, 20000, true),
            new Budget("September", "Education", 35000, 32000, true),
            new Budget("December", "Gifts", 30000, 28000, true),
            new Budget("January", "Healthcare", 22000, 20000, true)
        };

        var marek = new User(Guid.NewGuid(), "marek@gmail.com", "password", "user", DateTime.Now);
        var marekBudgets = new List<Budget>()
        {
            new Budget("April", "Selfcare", 14000, 12000, false),
            new Budget("June", "Entertainment", 28000, 29010, false),
            new Budget("August", "Utilities", 20000, 55000, false),
            new Budget("May", "Groceries", 22000, 20000, true),
            new Budget("October", "Travel", 42000, 40000, false),
            new Budget("July", "Transportation", 26000, 24500, true),
            new Budget("December", "Gifts", 32000, 30000, false),
            new Budget("September", "Education", 38000, 45500, true),
            new Budget("January", "Healthcare", 24000, 31000, false),
            new Budget("November", "Dining Out", 28000, 26000, true)
        };


        _budgets = new Dictionary<User, IEnumerable<Budget>>()
        {
            { karol, karolBudgets },
            { konrad, konradBudgets },
            { marek, marekBudgets }
        };
    }

    public async Task<Dictionary<User, IEnumerable<Budget>>> FetchBudgetsAsync(UserId userId)
    {
        await Task.CompletedTask;

        var myUser = _budgets.Keys.FirstOrDefault(user => user.Id == userId);
        var myUserBudgets = myUser != null ? _budgets[myUser] : Enumerable.Empty<Budget>();

        var othersBudgets = _budgets.Where(pair => pair.Key != myUser);
        
        var sharedFilteredBudgets = new Dictionary<User, IEnumerable<Budget>>();
        foreach (var pair in othersBudgets)
        {  
            var sharedBudgets = pair.Value.Where(budget => budget.Shared);
            sharedFilteredBudgets.Add(pair.Key, sharedBudgets);
        }

        Dictionary<User, IEnumerable<Budget>> allBudgets = new();
        allBudgets.Add(myUser, myUserBudgets);
        foreach (var userBudget in sharedFilteredBudgets)
        {
            allBudgets.Add(userBudget.Key, userBudget.Value);
        }

        return allBudgets;
    }

    public async Task AddAsync(Budget newBudget, UserId userId)
    {
        await Task.CompletedTask;

        var userToUpdate = _budgets.Keys.FirstOrDefault(user => user.Id == userId);
        if (userToUpdate != null)
        {
            var existingBudgets = _budgets[userToUpdate].ToList();
            
            existingBudgets.Add(newBudget);
            
            _budgets[userToUpdate] = existingBudgets;
        }
    }
}
