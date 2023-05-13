using FamilyBudget.Application.Abstractions;

namespace FamilyBudget.Application.Commands;

public record CreateBudget(string Month, decimal Income, decimal Expenses) : ICommand;
