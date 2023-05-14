using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.Commands;
using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace FamilyBudget.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BudgetsController : ControllerBase
{
    [Authorize]
    [HttpGet]
    [SwaggerOperation("Get all budgets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<BudgetsDto>>> Get(
        [FromServices] IQueryHandler<FetchBudgets, IEnumerable<BudgetsDto>> fetchHandler)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var budgets = await fetchHandler.HandleAsync(new FetchBudgets() { UserId = Guid.Parse(userId) });

        return Ok(budgets);
    }

    [Authorize]
    [HttpPost]
    [SwaggerOperation("Create new budget")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Post(
        [FromServices] ICommandHandler<CreateBudget> createHandler,
        CreateBudget command
        )
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        await createHandler.HandleAsync(command with { UserId = Guid.Parse(userId) });
     
        return Ok();
    }
}