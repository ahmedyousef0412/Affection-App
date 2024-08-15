

namespace Affection.Contract.Authentication;
public record AuthResponse
(
    string Id ,
    string UserName ,
    string Email ,
    string KnowAs ,
    string Country ,
    string City ,
    string Gender ,
    DateTime DateOfBirth 
    
);
