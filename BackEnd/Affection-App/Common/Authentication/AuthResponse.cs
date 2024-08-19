

namespace Affection.Contract.Authentication;
public record AuthResponse
(
    string Id ,
    string? UserName ,
    string? Email ,
    string KnowAs ,
    string Country ,
    string City ,
    string Gender ,
    DateTime DateOfBirth ,
    string Token,
    int ExpiresIn,
    string RefreshToken,
    DateTime RefreshTokenExpiration,
    string? PhotoUrl
    
);
