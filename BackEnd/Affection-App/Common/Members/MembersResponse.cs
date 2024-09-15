

namespace Affection.Contract.Members;
public record MembersResponse(

  string Id,
  string UserName,
  string LookingFor,
  string Introduction,
  string Interests,
  string Country,
  string City,
  string Email,
  string? MainPhotoUrl,
  string KnowAs,
  string Gender,
  int Age,
  ICollection<PhotoResponse> Photos,
  DateTime CreatedOn,
  DateTime LastActive

);

