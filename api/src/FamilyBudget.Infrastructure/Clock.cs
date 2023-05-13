using FamilyBudget.Core.Abstractions;

namespace FamilyBudget.Infrastructure;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}