using FamilyBudget.Core.Exceptions;

namespace FamilyBudget.Application.Exceptions;

public sealed class UserNotExistException : CustomException
{
    public UserNotExistException() : base($"User doesn't exists.")
    {
    }
}