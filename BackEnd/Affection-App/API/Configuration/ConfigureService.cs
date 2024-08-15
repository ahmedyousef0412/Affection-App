

namespace Affection.API.Configuration;

public static class ConfigureService
{

    public static IServiceCollection AffectionApiDependeciesService(this IServiceCollection services  ,IConfiguration configuration)
    {
        services.AddControllers();

        services.AddSwaggerConfig();
        services.AddSingleton<DefaultUsers>();

        services.AddConnectionString(configuration);

        services.AddMapsterConfig();

        services.AddContract();

        services.Configure<DefaultUser>(configuration.GetSection(nameof(DefaultUser)));

        services.AddInfrastructureServices(configuration);
        services.AddIdentityService();


        return services;
    }

    private static IServiceCollection AddConnectionString(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found !");


        services.AddDbContext<ApplicationDbContext>(options =>
                                    options.UseSqlServer(connectionString));


        return services;
    }


    private static IServiceCollection AddIdentityService(this IServiceCollection services)
    {

        services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();


        //Password

        services.Configure<IdentityOptions>(options =>
        {

            options.Password.RequiredLength = 8;

            options.User.RequireUniqueEmail = true;

            //options.Lockout.MaxFailedAccessAttempts = 3;
            //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(4);
        });

        return services;
    }

    private static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
    {

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
    private static IServiceCollection AddMapsterConfig(this IServiceCollection services)
    {
        var mapConfig = TypeAdapterConfig.GlobalSettings;

        mapConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mapConfig));


        return services;
    }
}


