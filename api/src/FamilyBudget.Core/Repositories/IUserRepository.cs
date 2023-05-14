using FamilyBudget.Core.Entities;
using FamilyBudget.Core.ValueObjects;

namespace FamilyBudget.Core.Repositories;

public interface IUserRepository
{
    List<User> Users { get; }

    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User> GetByEmailAsync(Email email);
    Task<User> GetByIdAsync(UserId userId);
    Task AddAsync(User user);
}