using FamilyBudget.Application.Abstractions;

namespace FamilyBudget.Application.Commands;

public record CreateBudget(string Month, int CategoryId, decimal Income, decimal Expenses, bool Shared, Guid UserId) : ICommand;
