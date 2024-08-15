

namespace Affection.Domain.Entities;
public class ApplicationUser:IdentityUser
{

    public DateTime DateOfBirth { get; set; }
    public string KnowAs { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime LastActive { get; set; } = DateTime.Now;
    public Gender Gender { get; set; } 
    public string? Introduction { get; set; }
    public string? LookingFor { get; set; }
    public string? Intrestes { get; set; }
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public bool IsRestricted { get; set; }

    public ICollection<Photo> Photos { get; set; } =  [];


    public int GetAge()
    {
        return DateOfBirth.CalculateAge();
    }
}
