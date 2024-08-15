


namespace Affection.Infrastructure.Configuration;
public static class ConfigureService
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
    {

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
