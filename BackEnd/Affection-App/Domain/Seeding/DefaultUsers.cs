

namespace Affection.Domain.Seeding;


public class DefaultUsers(IOptions<DefaultUser> user)
{
    private readonly DefaultUser _user = user.Value;

    public async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
    {
        ApplicationUser admin = new()
        {
            UserName = "admin",
            Email = _user.Email,
            Gender = Gender.Male,
            KnowAs = "Konafa",
            Country = "Cairo",
            City = "Mansoura",
            DateOfBirth = new DateTime(1996, 12, 15),
            EmailConfirmed = true,
        };

        var user = await userManager.FindByEmailAsync(admin.Email);

        if (user is null)
        {
            await userManager.CreateAsync(admin, _user.Password);
            //Ensure the role exists
            var roleExists = await userManager.IsInRoleAsync(admin, AppRoles.Admin);

            if (!roleExists)
            {
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);

            }
        }
    }
}



