using FamilyBudget.Application.DTO;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Application.Queries.Handlers;

public static class Extensions
{
    public static BudgetDto AsDto(this Budget entity)
        => new()
        {
            Name = entity.Name,
            Amount = entity.Amount
        };
}