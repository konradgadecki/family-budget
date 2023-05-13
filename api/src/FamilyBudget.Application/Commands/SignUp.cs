using FamilyBudget.Application.Abstractions;

namespace FamilyBudget.Application.Commands;

public record SignUp(Guid UserId, string Email, string Password, string Role) : ICommand;
