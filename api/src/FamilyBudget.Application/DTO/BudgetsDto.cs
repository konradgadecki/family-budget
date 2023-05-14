namespace FamilyBudget.Application.DTO;

public class BudgetsDto
{
    public UserDto User { get; set; }
    public IEnumerable<BudgetDto> Budgets { get; set; }
}
