

namespace Affection.Infrastructure.Implementation;

internal class AuthService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager
        ,IJWTProvider jwt
       ,IHttpContextAccessor httpContext
     
        , ILogger<AuthService> logger) : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly IJWTProvider _jwt = jwt;
    private readonly IHttpContextAccessor _httpContext = httpContext;
    private readonly ILogger<AuthService> _logger = logger;
    private readonly int _refreshTokenExpiryDays = 14;
  

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
    public async Task<Result<AuthResponse>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {

        //Check User?
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
            return Result.Failure<AuthResponse>(UserError.InvalidCredentials);

        if(user.IsRestricted)
            return Result.Failure<AuthResponse>(UserError.RestrictedUser);

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

        if (result.Succeeded)
        {
            //Generate Token
            var response = await GenerateAuthResponseAsync(user);
            
            return Result.Success(response);
        }


        var error = result.IsNotAllowed
            ? UserError.EmailNotConfirmed
            : result.IsLockedOut
            ? UserError.RestrictedUser
            : UserError.InvalidCredentials;

        return Result.Failure<AuthResponse>(error);
    }

    public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = _jwt.ValidateToken(token, validateLifetime: false);

        if (userId is null)
            return Result.Failure<AuthResponse>(UserError.InvalidJwtToken);

        var user = await _userManager.FindByIdAsync(userId);    

        if(user is null)
            return Result.Failure<AuthResponse>(UserError.InvalidJwtToken);


        if(user.IsRestricted)
            return Result.Failure<AuthResponse>(UserError.RestrictedUser);


        if (user.LockoutEnd > DateTime.UtcNow)
            return Result.Failure<AuthResponse>(UserError.LockedUser);

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken && rt.IsActive);


        if (userRefreshToken is null)
            return Result.Failure<AuthResponse>(UserError.InvalidRefreshToken);


        userRefreshToken.RevokedOn = DateTime.UtcNow;

        var photoUrl = user.Photos?.FirstOrDefault(u => u.IsMain)?.Url;

        var roles = await GetUserRoles(user);


        var (newToken, expiresOn) = _jwt.GenerateToken(user, roles);

        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {

            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiresOn
        });

        await _userManager.UpdateAsync(user);

        var response =  new AuthResponse(user.Id, user.UserName, user.Email, user.KnowAs, user.Country, user.City, user.Gender.ToString(), user.DateOfBirth, newToken, expiresOn, newRefreshToken, refreshTokenExpiresOn,photoUrl);


        return Result.Success(response);


    }

    //ConfirmEmailAsync

    //ResendConfirmationEmailAsync

    //SendResetPasswordCodeAsync

    //ForgetPasswordAsync

    


    private async Task<AuthResponse> GenerateAuthResponseAsync(ApplicationUser user )
    {
        var photoUrl = user.Photos?.FirstOrDefault(u => u.IsMain)?.Url;
        var userRoles = await GetUserRoles(user);

        // Generate the JWT token and expiration time
        var (token, expiresIn) = _jwt.GenerateToken(user, userRoles);

        // Generate the refresh token and its expiration date
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiresOn = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);


        user.RefreshTokens.Add(new RefreshToken
        {

            Token = refreshToken,
            ExpiresOn = refreshTokenExpiresOn,
        });

        // Save the refresh token in an HTTP-only, Secure cookie
        //SetRefreshTokenCookie(refreshToken, refreshTokenExpiresOn);

        await _userManager.UpdateAsync(user);

        return new AuthResponse(user.Id, user.UserName, user.Email, user.KnowAs, user.Country, user.City, user.Gender.ToString(), user.DateOfBirth,token,expiresIn ,refreshToken ,refreshTokenExpiresOn,photoUrl);
    }

    private async Task<IEnumerable<string>> GetUserRoles(ApplicationUser user )
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        return (userRoles);
    }
  
    private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    private void SetRefreshTokenCookie(string refreshToken, DateTime refreshTokenExpiresOn)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true, // Prevents JavaScript access to the cookie
            Secure = true, // Ensures the cookie is only sent over HTTPS
            SameSite = SameSiteMode.Strict, // Helps mitigate CSRF attacks
            Expires = refreshTokenExpiresOn // Set the expiration date of the cookie
        };

       _httpContext.HttpContext?.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }


}
