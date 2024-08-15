
using Affection.Contract.Authentication;
using Affection.Domain.Abstraction;

namespace Affection.Application.Interfaces;
public interface IAuthService
{

    Task<Result> RgisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
