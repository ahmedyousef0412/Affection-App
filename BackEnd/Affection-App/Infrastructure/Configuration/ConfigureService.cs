

namespace Affection.Infrastructure.Configuration;
public static class ConfigureService
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
    {

        services.AddScoped<IAuthService, AuthService>();


        #region Identity

        services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();


        //Password

        services.Configure<IdentityOptions>(options =>
        {

            options.Password.RequiredLength = 8;

            options.User.RequireUniqueEmail = true;

            options.Lockout.MaxFailedAccessAttempts = 3;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(4);
        });
        #endregion

        #region JWT


        //Because I need only one instance 
        services.AddSingleton<IJWTProvider, JWTProvider>();

        services.AddOptions<JWTConfiguration>()
            .BindConfiguration(JWTConfiguration.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSettings = configuration.GetSection(JWTConfiguration.SectionName).Get<JWTConfiguration>();



        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
      .AddJwtBearer(o =>
      {
          o.SaveToken = true;
          o.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuerSigningKey = true,
              ValidateIssuer = true,
              ValidateAudience = true,
              ValidateLifetime = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
              ValidIssuer = jwtSettings?.Issuer,
              ValidAudience = jwtSettings?.Audience,
              ClockSkew = TimeSpan.Zero
          };
      });



        #endregion

        return services;
    }
}
