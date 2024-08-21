
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

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

#region Seed Roles and Users

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using var scope = scopeFactory.CreateScope();

var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();


await DefaultRoles.SeedRolesAsync(roleManger);

var defaultUsers = scope.ServiceProvider.GetRequiredService<DefaultUsers>();

await defaultUsers.SeedUsersAsync(userManager);




#endregion


app.MapControllers();

app.UseExceptionHandler();

app.Run();
