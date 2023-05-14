using FamilyBudget.Core.Entities;
using FamilyBudget.Core.ValueObjects;

namespace FamilyBudget.Core.Repositories;

public interface IBudgetRepository
{
    Task AddAsync(Budget budget, UserId userId);
    Task<Dictionary<User, IEnumerable<Budget>>> FetchBudgetsAsync(UserId userId);
}
