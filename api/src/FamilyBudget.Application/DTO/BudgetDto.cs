namespace FamilyBudget.Application.DTO;

public class BudgetDto
{
    public string Month { get; set; }
    public int? CategoryId { get; set; }
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public decimal NetIncome { get; set; }
    public bool Shared { get; set; }
}
