

namespace Affection.Contract.Authentication;
public record  RegisterRequest
(
    string Email,
    string Password,
    string ConfirmPassword,
    string Gender,
    DateTime DateOfBirth,
    string KnowAs,
    string UserName,
    string Country,
    string City
);

