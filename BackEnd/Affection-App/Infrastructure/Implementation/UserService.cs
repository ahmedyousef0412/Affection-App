using Affection.Contract.Users;



namespace Affection.Infrastructure.Implementation
{
    public class UserService(UserManager<ApplicationUser> userManager) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;

      

        public async Task<Result<UserProfileResponse>> GetProfileAsync(string userId)
        {
            var user = await _userManager.Users.Where(u => u.Id == userId)
                .Include(u =>u.Photos)
                .SingleAsync();

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
}
