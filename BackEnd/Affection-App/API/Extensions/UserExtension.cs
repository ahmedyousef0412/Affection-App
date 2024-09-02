using System.Security.Claims;

namespace Affection.API.Extensions
{
    public static  class UserExtension
    {

        public static string? GetUserId(this ClaimsPrincipal claims)
        {
            return claims.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
