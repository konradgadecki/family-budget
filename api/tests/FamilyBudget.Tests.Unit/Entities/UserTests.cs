using System;
using FamilyBudget.Core.Entities;
using FamilyBudget.Core.Exceptions;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Unit.Entities;

public class UserTests
{

    [Fact]
    public void given_user_for_wrong_email_address_should_fail()
    {
        var wrongEmail = "wrongEmail";

        var exception = Record.Exception(() => new User(Guid.NewGuid(), wrongEmail, "somepass", "user", DateTime.Now)); ;

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidEmailException>();
    }

    [Fact]
    public void given_user_for_right_email_address_should_success()
    {
        var rightEmail = "email@gmail.com";

        var exception = Record.Exception(() => new User(Guid.NewGuid(), rightEmail, "somepass", "user", DateTime.Now)); ;

        exception.ShouldBeNull();
        exception.ShouldNotBeOfType<InvalidEmailException>();
    }

    [Fact]
    public void given_user_for_wrong_role_should_fail()
    {
        var wrongRole = "wrongrole";

        var exception = Record.Exception(() => new User(Guid.NewGuid(), "email@email.com", "somepass", wrongRole, DateTime.Now));

        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidRoleException>();
    }

    [Fact]
    public void given_user_for_right_role_should_success()
    {
        var rightRole = "user";

        var exception = Record.Exception(() => new User(Guid.NewGuid(), "email@email.com", "somepass", rightRole, DateTime.Now));

        exception.ShouldBeNull();
    }
}