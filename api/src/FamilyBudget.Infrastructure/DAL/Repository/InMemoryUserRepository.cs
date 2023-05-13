using FamilyBudget.Core.Abstractions;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;
using FamilyBudget.Core.ValueObjects;

namespace FamilyBudget.Infrastructure.DAL.Repository
{
    internal class InMemoryUserRepository : IUserRepository
    {
        private const string PASSWORD = "AQAAAAIAAYagAAAAENLG7z25ckkVhX58acf0u5UkUacf2xdp4YfBoi1uoSGQ3f9raKXYFalMdocIAkhxRA=="; //password
        private const string KOGA_PASS = "AQAAAAIAAYagAAAAED7fGHRSw/TWJETt3oh3wEOyo5oHUVHmZEm/s3AFxA7QkA8Y/BQjCIdjgnLTVogm2w==";

        private readonly List<User> _users;

        public InMemoryUserRepository(IClock clock)
        {
            _users = new List<User>()
            {
                new User(Guid.NewGuid(), "johndoe@familybudget.com", PASSWORD, "admin", clock.Current()),
                new User(Guid.NewGuid(), "michaeljackson@familybudget.com", PASSWORD, "user", clock.Current()),
                new User(Guid.NewGuid(), "freddiemercury@familybudget.com", PASSWORD, "user", clock.Current()),
                new User(Guid.NewGuid(), "koga@gmail.com", KOGA_PASS, "user", clock.Current())
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
