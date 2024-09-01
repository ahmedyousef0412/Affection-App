

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AffectionApiDependeciesService(builder.Configuration);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseHangfireDashboard("/jobs", new DashboardOptions
{

    Authorization =
    [
        new HangfireCustomBasicAuthenticationFilter
        {
            User = app.Configuration.GetValue<string>("HangfireSettings:Username"),
            Pass = app.Configuration.GetValue<string>("HangfireSettings:Password")
        }
    ],
    DashboardTitle = "Affection App Dashboard",
    //IsReadOnlyFunc = (DashboardContext context) => true
});


app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

#region Seed Roles and Users
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using var scope = scopeFactory.CreateScope();

var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
var userConfig = scope.ServiceProvider.GetRequiredService<IOptions<DefaultUser>>().Value;
var defaultUsers = scope.ServiceProvider.GetRequiredService<DefaultUsers>(); // Get DefaultUsers instance

// Seed roles and users
await DefaultRoles.SeedRolesAsync(roleManager);
await defaultUsers.SeedUsersAsync(userManager);



#endregion


app.MapControllers();

app.UseExceptionHandler();

app.Run();
