using FamilyBudget.Core.Entities;
using FamilyBudget.Core.ValueObjects;
using System.Collections.Immutable;

namespace FamilyBudget.Core.Repositories;

public interface IBudgetRepository
{
    Task AddAsync(Budget budget, UserId userId);
    Task<Category> GetCategoryById(int categoryId);
    Task<IImmutableList<Budget>> FetchBudgetsAsync(UserId userId);
    Task<IImmutableList<Category>> GetAllCategoriesAsync();
}
