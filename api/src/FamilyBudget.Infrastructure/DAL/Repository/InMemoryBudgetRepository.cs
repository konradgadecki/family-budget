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
            new Budget("April", 15000),
            new Budget("May", 10000),
            new Budget("June", 20000),
        };
    }

    public async Task<IEnumerable<Budget>> FetchBudgetsAsync()
    {
        await Task.CompletedTask;

        return _budgets;
    }
}
