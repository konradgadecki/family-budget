using FamilyBudget.Core.Entities;

namespace FamilyBudget.Core.Repositories;

public interface IBudgetRepository
{
    Task<IEnumerable<Budget>> FetchBudgetsAsync();
}
