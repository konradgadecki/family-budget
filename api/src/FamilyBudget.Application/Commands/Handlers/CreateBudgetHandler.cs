using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.Exceptions;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Repositories;

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
            throw new UserDoesNotExistException();
        }

        var category = await _budgetRepository.GetCategoryById(command.CategoryId);
        if (category is null) 
        { 
            throw new CategoryDoesNotExistException();
        }

        var budget = new Budget(Guid.NewGuid(), command.UserId, category.Id, command.Month, command.Income, command.Expenses, command.Shared);

        await _budgetRepository.AddAsync(budget, command.UserId);
    }
}