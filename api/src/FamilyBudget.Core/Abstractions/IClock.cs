namespace FamilyBudget.Core.Abstractions;

public interface IClock
{
    DateTime Current();
}