using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers;

[Route("")]
public class HomeController : ControllerBase
{    
    private readonly string APP_NAME = "Family Budget";

    [HttpGet]
    public ActionResult Get() => Ok(APP_NAME);
}