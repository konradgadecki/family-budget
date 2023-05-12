using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;
using FamilyBudget.Core.ValueObjects;

namespace FamilyBudget.Infrastructure.DAL.Repository
{
    internal class InMemoryUserRepository : IUserRepository
    {
        private readonly IEnumerable<User> _users;

        public InMemoryUserRepository()
        {
            _users = new List<User>()
            {
                new User(Guid.NewGuid(), "johndoe@familybudget.com", "johndoe", "password", "John Doe", "admin", DateTime.UtcNow)
            };
        }
        
        public async Task<User> GetByEmailAsync(Email email)
        {
            await Task.CompletedTask;

            return _users.SingleOrDefault(x => x.Email == email);
        }
    }
}
