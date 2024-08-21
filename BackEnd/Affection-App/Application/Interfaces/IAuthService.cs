
namespace Affection.Application.Interfaces;
public interface IAuthService
{

    Task<Result> RgisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default);
    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
    Task<Result> ResendConfirmationEmailAsync(ResendConfirmationEmail request);
    Task<Result> SendResetPasswordCodeAsync(ForgetPasswordRequest request);
    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
}
