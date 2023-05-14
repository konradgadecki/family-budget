namespace FamilyBudget.Core.Entities;

public class Category
{
    public int Id { get; set; }
    public Guid BudgetId { get; set; }
    public string Name { get; set; }

    public Category(int id, string name)
    {
        Id = id;
        Name = name;
    }
}