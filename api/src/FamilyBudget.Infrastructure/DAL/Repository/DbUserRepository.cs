using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;
using FamilyBudget.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Infrastructure.DAL.Repository;

internal class DbUserRepository : IUserRepository
{
    private readonly FamilyBudgetDbContext _dbContext;

    public List<User> Users => throw new NotImplementedException();

    public DbUserRepository(FamilyBudgetDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
        => await _dbContext.Users.ToListAsync();

    public Task<User> GetByEmailAsync(Email email)
        => _dbContext.Users.SingleOrDefaultAsync(x => x.Email == email);

    public Task<User> GetByIdAsync(UserId id)
        => _dbContext.Users.SingleOrDefaultAsync(x => x.Id == id);
}