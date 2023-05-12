using FamilyBudget.Core.Exceptions;

namespace FamilyBudget.Application.Exceptions;

public class InvalidCredentialsException : CustomException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}