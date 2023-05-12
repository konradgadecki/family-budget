using FamilyBudget.Application.Abstractions;

namespace FamilyBudget.Application.Commands;

public record SignIn(string Email, string Password) : ICommand;
