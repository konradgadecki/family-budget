using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.Auth;
using FamilyBudget.Application.Exceptions;
using FamilyBudget.Core.Abstractions;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;
using FamilyBudget.Core.ValueObjects;

namespace FamilyBudget.Application.Commands.Handlers;

internal sealed class CreateBudgetHandler : ICommandHandler<CreateBudget>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly IUserRepository _userRepository;

    public CreateBudgetHandler(IBudgetRepository budgetRepository, IUserRepository userRepository)
    {
        _budgetRepository = budgetRepository;
        _userRepository = userRepository;
    }

    public async Task HandleAsync(CreateBudget command)
    {
        if (await _userRepository.GetByIdAsync(command.UserId) is null) 
        {
            throw new UserNotExistException();
        }

        var budget = new Budget(command.Month, command.Category, command.Income, command.Expenses, command.Shared);

        await _budgetRepository.AddAsync(budget, command.UserId);
    }
}