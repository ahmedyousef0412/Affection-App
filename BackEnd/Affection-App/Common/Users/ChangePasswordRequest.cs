
namespace Affection.Contract.Users;
public record ChangePasswordRequest(
    string CurrentPassword,
    string NewPassword
);
