

using System.ComponentModel.DataAnnotations;

namespace Affection.Infrastructure.Configuration;
public sealed class JWTConfiguration
{

    public static string SectionName = "JWT";

    [Required]
    public string Key { get; init; } = string.Empty;

    [Required]
    public string Issuer { get; init; } = string.Empty;

    [Required]
    public string Audience { get; init; } = string.Empty;

    [Range(1, 60)]
    public int ExpireInMinute { get; init; }


}
