using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;

namespace FamilyBudget.Infrastructure.DAL.Repository;

internal class InMemoryBudgetRepository : IBudgetRepository
{
    private readonly List<Budget> _budgets;

    public InMemoryBudgetRepository()
    {
        _budgets = new List<Budget>()
        {
            new Budget("April", "Selfcare", 15000, 14000),
            new Budget("May", "Travel", 10000, 12500),
            new Budget("June", "Health", 20000, 14000),
            new Budget("June", "Entertainment", 1500, 14000),
            new Budget("June", "Work", 45000, 140),
        };
    }

    public async Task<IEnumerable<Budget>> FetchBudgetsAsync()
    {
        await Task.CompletedTask;

        return _budgets;
    }
}
