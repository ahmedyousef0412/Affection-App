

namespace Affection.API.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]


public class MembersController(IMemberService memberService , IPhotoService photoService ) : ControllerBase
{
    private readonly IMemberService _memberService = memberService;
    private readonly IPhotoService _photoService = photoService;

    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromQuery ]RequestFilter filters, CancellationToken cancellationToken)
    {
        var user = await _memberService.GetUserByIdAsync(User.GetUserId()!,cancellationToken);

        filters.UserId = user.Value.Id;

      

        var result =  await _memberService.GetAllAsync(filters, cancellationToken);


        return result.IsSuccess ? Ok(result.Value) : result.ToProblem(); 
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute]string id ,CancellationToken cancellationToken)
    {
         
        var result = await _memberService.GetUserByIdAsync(id!,cancellationToken);


        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }



    [HttpGet("photos/{id}")]
    public async Task<IActionResult> GetPhotosByUserId([FromRoute] string id, CancellationToken cancellationToken)
    {

        return Ok(await _photoService.GetPhotosByUserId(id, cancellationToken));
    }

}
