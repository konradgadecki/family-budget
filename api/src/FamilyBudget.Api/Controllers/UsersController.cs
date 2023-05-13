using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.Auth;
using FamilyBudget.Application.Commands;
using FamilyBudget.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FamilyBudget.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet("{userId:guid}")]
    [SwaggerOperation("Get a user by id")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserDto> Get(Guid userId)
    {
        var user = new UserDto() { Id = userId, Username = "Konrad" };

        return user;
    }

    [HttpPost("sign-in")]
    [SwaggerOperation("Sign the user in and return the JSON Web Token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post(
        [FromServices] ICommandHandler<SignIn> signInHandler,
        [FromServices] ITokenStorage _tokenStorage,
        SignIn command)
    {
        await signInHandler.HandleAsync(command);

        var jwt = _tokenStorage.Get();
        return jwt;
    }

    [HttpPost("sign-up")]
    [SwaggerOperation("Sign up the user")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<JwtDto>> Post(
        [FromServices] ICommandHandler<SignUp> signUpHandler,
        SignUp command)
    {
        command = command with { UserId = Guid.NewGuid() };
        await signUpHandler.HandleAsync(command);

        return NoContent();
    }
}