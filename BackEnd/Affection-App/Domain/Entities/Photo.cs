﻿

namespace Affection.Domain.Entities;
public class Photo
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public bool IsMain { get; set; }
    public string PublicId { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = new ApplicationUser();
}
