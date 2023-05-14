using FamilyBudget.Core.Exceptions;

namespace FamilyBudget.Application.Exceptions;

public sealed class CategoryDoesNotExistException : CustomException
{
    public CategoryDoesNotExistException() : base($"Category doesn't exists.")
    {
    }
}