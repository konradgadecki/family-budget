using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.DTO;

namespace FamilyBudget.Application.Queries;

public class FetchBudgets : IQuery<IEnumerable<BudgetsDto>>
{
    public Guid UserId { get; set; }
}