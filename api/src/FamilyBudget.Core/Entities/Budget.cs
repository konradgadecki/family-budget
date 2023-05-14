using FamilyBudget.Core.ValueObjects;

namespace FamilyBudget.Core.Entities;

public class Budget
{
    public Guid Id { get; set; }
    public UserId UserId { get; set; }
    public int CategoryId { get; set; }
    public string Month { get; set; }
    public decimal Income { get; set; }
    public decimal Expenses { get; set; }
    public bool Shared { get; set; }

    public Budget(Guid id, UserId userId, int categoryId, string month, decimal income, decimal expenses, bool shared)
    {
        Id = id;
        UserId = userId;
        CategoryId = categoryId;
        Month = month;
        Income = income;
        Expenses = expenses;
        Shared = shared;
    }
}