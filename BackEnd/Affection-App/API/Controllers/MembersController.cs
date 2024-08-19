
using Affection.Domain.Const;
using Microsoft.AspNetCore.Authorization;

namespace Affection.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = AppRoles.Admin)]
public class MembersController : ControllerBase
{

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }
}
