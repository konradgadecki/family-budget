using FamilyBudget.Core.Abstractions;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;
using FamilyBudget.Core.ValueObjects;

namespace FamilyBudget.Infrastructure.DAL.Repository
{
    internal class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users;

        public InMemoryUserRepository(IClock clock)
        {
            _users = new List<User>()
            {
                new User(Guid.NewGuid(), "johndoe@familybudget.com", "password", "admin", clock.Current()),
                new User(Guid.NewGuid(), "michaeljackson@familybudget.com", "password", "user", clock.Current()),
                new User(Guid.NewGuid(), "freddiemercury@familybudget.com", "password", "user", clock.Current())
            };
        }

        public async Task<User> GetByEmailAsync(Email email)
        {
            await Task.CompletedTask;

            return _users.SingleOrDefault(x => x.Email == email);
        }

        public Task AddAsync(User user)
        {
            _users.Add(user);
         
            return Task.CompletedTask;
        }
    }
}
