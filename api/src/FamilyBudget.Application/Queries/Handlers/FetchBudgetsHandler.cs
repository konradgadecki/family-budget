using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.DTO;
using FamilyBudget.Core.Repositories;

namespace FamilyBudget.Application.Queries.Handlers;

internal class FetchBudgetsHandler : IQueryHandler<FetchBudgets, IEnumerable<BudgetsDto>>
{
    private readonly IBudgetRepository _repository;

    public FetchBudgetsHandler(IBudgetRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<BudgetsDto>> HandleAsync(FetchBudgets query)
    {
        var budgets = await _repository.FetchBudgetsAsync(query.UserId);

        return budgets.Select(x => new BudgetsDto() 
        { 
            User = new UserDto()
            {
                Id = x.Key.Id,
                Email = x.Key.Email
            },
            Budgets = x.Value.Select(x => x.AsDto())
        });
    }
}
