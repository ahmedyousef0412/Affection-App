

namespace Affection.Infrastructure.Implementation;

public class UserService(UserManager<ApplicationUser> userManager, ICacheService cacheService) : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ICacheService _cacheService = cacheService;

    public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId)
    {
        var cacheKey = $"UserProfile_{userId}";

        var cacheProfile = _cacheService.GetData<UserProfileResponse>(cacheKey);


        if (cacheProfile is not null)
            return Result.Success(cacheProfile);


        var user = await _userManager.Users.Where(u => u.Id == userId)
            .Include(u => u.Photos)
            .AsNoTracking()
            .SingleAsync();


        if (user is null)
            return Result.Failure<UserProfileResponse>(UserError.UserNotFound);



        var mainPhotoUrl = user.Photos.FirstOrDefault(p => p.IsMain)?.Url;

        var userProfile = new UserProfileResponse(
            user.LookingFor,
            user.Introduction,
            user.Interests,
            user.Country,
            user.City,
            user.Email,
            user.KnowAs,
            user.Gender.ToString(),
            user.DateOfBirth,
            mainPhotoUrl
        );


        // Cache the data
        var expirationTime = DateTimeOffset.Now.AddHours(1); // Cache for 1 hour
        _cacheService.SetData(cacheKey, userProfile, expirationTime);

        return Result.Success(userProfile);
    }

    public async Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request)
    {
        await _userManager.Users.Where(u => u.Id == userId)
             .ExecuteUpdateAsync(setter =>

                 setter
                 .SetProperty(u => u.City, request.City)
                 .SetProperty(u => u.Country, request.Country)
                 .SetProperty(u => u.LookingFor, request.LookingFor)
                 .SetProperty(u => u.Introduction, request.Introduction)
                 .SetProperty(u => u.Interests, request.Intrestes)
             );



        var cacheKey = $"UserProfile_{userId}";

        // Remove the cached data for this user
        _cacheService.RemoveData(cacheKey);

        return Result.Success();
    }


    public async Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        if (result.Succeeded)
            return Result.Success();

        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
}
