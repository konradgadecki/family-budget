using FamilyBudget.Application.DTO;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Application.Queries.Handlers;

public static class Extensions
{
    public static BudgetDto AsDto(this Budget entity, int? categoryId)
        => new()
        {
            Month = entity.Month,
            CategoryId = categoryId,
            Income = entity.Income,
            Expenses = entity.Expenses,
            NetIncome = entity.Income - entity.Expenses,
            Shared = entity.Shared,
        };

    public static CategoryDto AsDto(this Category entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name
        };
}