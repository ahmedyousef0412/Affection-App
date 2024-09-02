

namespace Affection.Contract.Users;
public record UserProfileResponse(

  string LookingFor,
  string Introduction,
  string Intrestes,
  string Country,
  string City,
  string Email,
  string KnowAs,
  string Gender,
  DateTime DateOfBirth,
  string? MainPhotoUrl

);

