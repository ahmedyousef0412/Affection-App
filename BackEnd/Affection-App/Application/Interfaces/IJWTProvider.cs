
namespace Affection.Application.Interfaces;
public interface IJWTProvider
{
    (string token, int expiresIn) GenerateToken(ApplicationUser user , IEnumerable<string> roles);

    string? ValidateToken(string token, bool validateLifetime = true);

}
