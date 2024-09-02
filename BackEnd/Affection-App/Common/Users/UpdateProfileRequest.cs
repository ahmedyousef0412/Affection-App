

namespace Affection.Contract.Users;
public record UpdateProfileRequest(

   string LookingFor,
   string Introduction,
   string Intrestes,
   string Country,
   string City
);

