
namespace Affection.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = AppRoles.Admin)]
public class MembersController : ControllerBase
{

    [HttpGet("")]
    public async Task<IActionResult> GetAll()
    {
        var response = new { message = "hello" }; // Return as JSON object
        return Ok(response);
    }
}
