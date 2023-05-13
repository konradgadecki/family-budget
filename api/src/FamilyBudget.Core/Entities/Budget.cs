namespace FamilyBudget.Core.Entities;

public class Budget
{

    public string Name { get; set; }
    public decimal Amount { get; set; }

    public Budget(string name, decimal amount)
    {
        Name = name;
        Amount = amount;
    }
}