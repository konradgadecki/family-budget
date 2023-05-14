using FamilyBudget.Core.Exceptions;

namespace FamilyBudget.Application.Exceptions;

public sealed class UserDoesNotExistException : CustomException
{
    public UserDoesNotExistException() : base($"User doesn't exists.")
    {
    }
}
