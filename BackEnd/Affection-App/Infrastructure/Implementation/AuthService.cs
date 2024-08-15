

namespace Affection.Infrastructure.Implementation;


internal class AuthService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext context
        , ILogger<AuthService> logger) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly ApplicationDbContext _context = context;
    private readonly ILogger<AuthService> _logger = logger;

  

    public async Task<Result> RgisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var emailIsExist = await _userManager.Users.AnyAsync(u => u.Email == request.Email, cancellationToken);

        if (emailIsExist)
            return Result.Failure(UserError.DuplicatedEmail);

        var user = request.Adapt<ApplicationUser>();


        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            _logger.LogInformation("Confirmation code : {code}", code);

            return Result.Success();
        }


        var error = result.Errors.First();

        return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
    }
    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

       

        if (user is null)
            return Result.Failure<AuthResponse>(UserError.InvalidCredentials);

        if(user.IsRestricted)
            return Result.Failure<AuthResponse>(UserError.RestrictedUser);

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

        if (result.Succeeded)
        {

            var response = await GenerateAuthResponseAsync(user, cancellationToken);
            
            return Result.Success(response);
        }


        var error = result.IsNotAllowed
            ? UserError.EmailNotConfirmed
            : result.IsLockedOut
            ? UserError.RestrictedUser
            : UserError.InvalidCredentials;

        return Result.Failure<AuthResponse>(error);
    }

    private async Task<AuthResponse> GenerateAuthResponseAsync(ApplicationUser user, CancellationToken cancellationToken)
    {
        return new AuthResponse(user.Id, user.UserName, user.Email, user.KnowAs, user.Country, user.City, user.Gender.ToString(), user.DateOfBirth);
    }
}
