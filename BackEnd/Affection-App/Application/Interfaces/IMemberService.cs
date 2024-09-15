


namespace Affection.Application.Interfaces;
public interface IMemberService
{

    Task<Result<PaginatedList<MembersResponse>>> GetAllAsync(RequestFilter filters, CancellationToken cancellationToken = default);
    Task<Result<MemberResponse>> GetUserByIdAsync(string id, CancellationToken cancellationToken = default);
}
