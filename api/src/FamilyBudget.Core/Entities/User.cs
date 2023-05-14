using FamilyBudget.Core.ValueObjects;

namespace FamilyBudget.Core.Entities;

public class User
{
    public UserId Id { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public Role Role { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public User(UserId id, Email email, Password password, Role role,  DateTime createdAt)
    {
        Id = id;
        Email = email;
        Password = password;
        Role = role;
        CreatedAt = createdAt;
    }
}