using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;
using FamilyBudget.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace FamilyBudget.Infrastructure.DAL.Repository;

internal class DbBudgetRepository : IBudgetRepository
{
    private readonly FamilyBudgetDbContext _dbContext;

    private readonly IUserRepository _userRepository;

    public DbBudgetRepository(FamilyBudgetDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
    }

    public async Task<IImmutableList<Budget>> FetchBudgetsAsync(UserId userId)
    {
        var myBudgets = await _dbContext.Budgets.Where(b => b.UserId == userId).ToListAsync();
        var othersBudgets = await _dbContext.Budgets.Where(b => b.UserId != userId && b.Shared).ToListAsync();

        return myBudgets.Concat(othersBudgets).ToImmutableArray(); 
    }

    public async Task AddAsync(Budget newBudget, UserId userId)
    {
        await _dbContext.Budgets.AddAsync(newBudget);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Category> GetCategoryById(int categoryId)
        => await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);


    public async Task<IImmutableList<Category>> GetAllCategoriesAsync()
        => (await _dbContext.Categories.ToListAsync()).ToImmutableList();
}
