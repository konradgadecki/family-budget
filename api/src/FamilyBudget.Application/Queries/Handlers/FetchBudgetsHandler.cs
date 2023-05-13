using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.DTO;
using FamilyBudget.Core.Repositories;

namespace FamilyBudget.Application.Queries.Handlers;

internal class FetchBudgetsHandler : IQueryHandler<FetchBudgets, IEnumerable<BudgetDto>>
{
    private readonly IBudgetRepository _repository;

    public FetchBudgetsHandler(IBudgetRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<BudgetDto>> HandleAsync(FetchBudgets query)
    {
        var budgets = await _repository.FetchBudgetsAsync();

        return budgets.Select(x => x.AsDto());
    }
}
