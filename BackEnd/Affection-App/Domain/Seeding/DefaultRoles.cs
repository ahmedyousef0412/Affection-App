


namespace Affection.Domain.Seeding;
public static class DefaultRoles
{

    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Moderator));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Member));
        }
    }
}
