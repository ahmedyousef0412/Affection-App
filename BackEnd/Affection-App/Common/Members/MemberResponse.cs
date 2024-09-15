

namespace Affection.Contract.Members;
public class MemberResponse
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string? LookingFor { get; set; }
    public string? Introduction { get; set; }
    public string? Interests { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? MainPhotoUrl { get; set; }
    public string KnowAs { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int Age { get; set; }
    public ICollection<PhotoResponse> Photos { get; set; } = [];
    public DateTime CreatedOn { get; set; }
    public DateTime LastActive { get; set; }
}
