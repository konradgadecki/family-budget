using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Exceptions;
using FamilyBudget.Core.Repositories;

namespace FamilyBudget.Application.Queries.Handlers;

internal class FetchBudgetsHandler : IQueryHandler<FetchBudgets, IEnumerable<BudgetsDto>>
{
    private readonly IBudgetRepository _repository;
    private readonly IUserRepository _userRepository;

    public FetchBudgetsHandler(IBudgetRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<BudgetsDto>> HandleAsync(FetchBudgets query)
    {
        var budgets = await _repository.FetchBudgetsAsync(query.UserId);
        var categories = await _repository.GetAllCategoriesAsync();
        var users = await _userRepository.GetAllUsersAsync();

        var budgetsPerUser = budgets.GroupBy
            (b => b.UserId, (key, budget) =>
            new { UserId = key, Budget = budget });
         

        return budgetsPerUser.Select(budgetPerUser =>
        {
            var user = users.FirstOrDefault(user => user.Id == budgetPerUser.UserId);
            if (user == null)
            {
                throw new UserDoesNotExistException();
            }

            return new BudgetsDto()
            {
                User = new UserDto()
                {
                    Id = user.Id,
                    Email = user.Email
                },
                Budgets = budgetPerUser.Budget.Select(budget => budget.AsDto(categories.FirstOrDefault(cat => cat.Id == budget.CategoryId)?.Id))
            };
        });
    }
}
