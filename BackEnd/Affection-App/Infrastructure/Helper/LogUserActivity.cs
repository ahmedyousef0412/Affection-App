

using Microsoft.AspNetCore.Mvc.Filters;

namespace Affection.Infrastructure.Helper;
public class LogUserActivity(ILogger<LogUserActivity> logger,ApplicationDbContext context) : IAsyncActionFilter
{
    private readonly ILogger<LogUserActivity> _logger = logger;
    private readonly ApplicationDbContext _context = context;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next();


        if (!resultContext.HttpContext.User.Identity!.IsAuthenticated)
            return;

        // Retrieve the user's ID from the claims
        var userId = resultContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            _logger.LogWarning("User ID claim is missing in the token.");
            return;
        }

        var user = await _context.Users.FindAsync(userId);

        if (user is null)
        {
            _logger.LogWarning("User not found with ID: {UserId}", userId);
            return;
        }

        user.LastActive = DateTime.Now;


        var saveResult = await _context.SaveChangesAsync();

        if (saveResult > 0)
        {
            _logger.LogInformation("User {UserName}'s last active time was updated.", user.UserName);
        }
        else
        {
            _logger.LogError("Failed to update last active time for user {UserName}.", user.UserName);
        }

     
    }
}
