using FamilyBudget.Application.DTO;
using FamilyBudget.Core.Entities;

namespace FamilyBudget.Application.Queries.Handlers;

public static class Extensions
{
    public static BudgetDto AsDto(this Budget entity)
        => new()
        {
            Month = entity.Month,
            Category = entity.Category,
            Income = entity.Income,
            Expenses = entity.Expenses,
            NetIncome = entity.Income - entity.Expenses
        };
}