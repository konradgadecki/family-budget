using FamilyBudget.Application.Abstractions;
using FamilyBudget.Application.DTO;
using FamilyBudget.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FamilyBudget.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController : ControllerBase
{
    [Authorize]
    [HttpGet]
    [SwaggerOperation("Get all categories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get(
        [FromServices] IQueryHandler<FetchCategories, IEnumerable<CategoryDto>> fetchHandler)
    {
        var categories = await fetchHandler.HandleAsync(new FetchCategories() );

        return Ok(categories);
    }
}