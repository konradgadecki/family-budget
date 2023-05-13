using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.Commands;
using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    public async Task<ActionResult<IEnumerable<BudgetDto>>> Get(
        [FromServices] IQueryHandler<FetchBudgets, IEnumerable<BudgetDto>> fetchHandler)
    {
        var budgets = await fetchHandler.HandleAsync(new FetchBudgets());

        return Ok(budgets);
    }

    [Authorize]
    [HttpPost]
    [SwaggerOperation("Create new budget")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<BudgetDto>>> Post(
        //[FromServices] ICommandHandler<CreateBudget> createHandler,
        CreateBudget command
        )
    {
        //var budgets = await fetchHandler.HandleAsync(new FetchBudgets());

        await Task.CompletedTask;
        return Ok();
    }
}