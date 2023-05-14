using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.DTO;
using FamilyBudget.Core.Repositories;

namespace FamilyBudget.Application.Queries.Handlers;

internal class FetchCategoriesHandler : IQueryHandler<FetchCategories, IEnumerable<CategoryDto>>
{
    private readonly IBudgetRepository _repository;

    public FetchCategoriesHandler(IBudgetRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> HandleAsync(FetchCategories query)
    {
        await Task.CompletedTask;

        var categories = await _repository.GetAllCategoriesAsync();

        return categories.Select(x => x.AsDto());
    }
}

