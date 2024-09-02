

using Affection.Contract.Photos;

namespace Affection.API.Controllers;
[Route("me")]
[ApiController]
[Authorize]
public class AccountController(IUserService userService , IPhotoService photoService) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IPhotoService _photoService = photoService;

    [HttpGet("")]
    public async Task<IActionResult> Info()
    {
        var result = await _userService.GetProfileAsync(User.GetUserId()!);

        return Ok(result.Value);
    }

    [HttpPut("info")]
    public async Task<IActionResult> Info( [FromBody] UpdateProfileRequest request)
    {
         await _userService.UpdateProfileAsync(User.GetUserId()! ,request);

        return NoContent();
    }

    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var result = await _userService.ChangePasswordAsync(User.GetUserId()!, request);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPost("upload-photo")]
    public async Task<IActionResult> UploadPhoto( IFormFile file ,CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var result = await _photoService.UploadPhotoAsync(file, userId!, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

    [HttpPut("set-photo-main/{photoId}")]
    public async Task<IActionResult> SetPhotoMain( int photoId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var result = await _photoService.SetPhotoMainAsync(userId!,photoId, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
    [HttpDelete("delete-photo/{photoId}")]
    public async Task<IActionResult> DeletePhoto(int photoId, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId();

        var result = await _photoService.DeletePhotoAsync(userId!, photoId, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }

}
