using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LRSchoolV2.Blazor.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class HealthcheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok();
}