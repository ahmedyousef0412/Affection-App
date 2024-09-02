
using Affection.Contract.Users;

namespace Affection.Application.Interfaces;
public interface IUserService
{

    Task<Result<UserProfileResponse>> GetProfileAsync(string userId);

    Task<Result> UpdateProfileAsync(string userId, UpdateProfileRequest request);

    Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequest request);
}
