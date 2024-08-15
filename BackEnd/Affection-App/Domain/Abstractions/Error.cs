

namespace Affection.Domain.Abstraction;
public record Error(string Code, string Description, int? StautsCode)
{
    public static readonly Error None = new(string.Empty, string.Empty, null);
}
