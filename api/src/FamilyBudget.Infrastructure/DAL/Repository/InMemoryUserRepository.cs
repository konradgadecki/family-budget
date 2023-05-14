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

        public List<User> Users { get; private set; }

        public InMemoryUserRepository(IClock clock)
        {
            Users = new List<User>()
            {
                new User(Guid.Parse("77777777-7777-7777-7777-777777777777"), "koga@gmail.com", KOGA_PASS, "user", clock.Current()),
                new User(Guid.NewGuid(), "karol@gmail.com", PASSWORD, "user", clock.Current()),
                new User(Guid.NewGuid(), "marek@gmail.com", PASSWORD, "user", clock.Current()),
                new User(Guid.NewGuid(), "admin@gmail.com", PASSWORD, "admin", clock.Current())
            };
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            await Task.CompletedTask;

            return Users;
        }

        public async Task<User> GetByEmailAsync(Email email)
        {
            await Task.CompletedTask;

            return Users.SingleOrDefault(x => x.Email == email);
        }

        public async Task<User> GetByIdAsync(UserId id)
        {
            await Task.CompletedTask;

            return Users.SingleOrDefault(x => x.Id == id);
        }

        public Task AddAsync(User user)
        {
            Users.Add(user);
         
            return Task.CompletedTask;
        }
    }
}
