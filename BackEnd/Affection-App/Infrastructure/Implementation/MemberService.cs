
namespace Affection.Infrastructure.Implementation;

public class MemberService(ApplicationDbContext context , ICacheService cacheService) : IMemberService
{
    private readonly ApplicationDbContext _context = context;
    private readonly ICacheService _cacheService = cacheService;
     private const string CacheKeyPrefix = "Members_";

    public async Task<Result<PaginatedList<MembersResponse>>> GetAllAsync(RequestFilter filters,CancellationToken cancellationToken = default)
    {

        //var cacheKey = $"{CacheKeyPrefix}{filters.PageNumber}_{filters.PageSize}" +
        //                             $"_{filters.Gender}_{filters.CurrentUserName}" +
        //                             $"_{filters.OrderBy}_{filters.MinAge}_{filters.MaxAge}";



        //var cachedMembers = _cacheService.GetData<List<MembersResponse>>(cacheKey);


        //if (cachedMembers is not null)
        //{
        //    var cachedPaginatedList = new PaginatedList<MembersResponse>(cachedMembers, filters.PageNumber, cachedMembers.Count, filters.PageSize);
        //    return Result.Success(cachedPaginatedList);
        //}



        var query = _context.Users.AsQueryable();


        if (Enum.TryParse<Gender>(filters.Gender, out var genderEnum))
        {
            query = query.Where(u => u.Gender == genderEnum);
        }
        else
        {

            return Result.Failure<PaginatedList<MembersResponse>>(MemberError.InvalidGender);
        }



        query = query.Where(u => u.Id != filters.UserId);



        var currentDate = DateTime.Today;


        var maxBirthdate = currentDate.AddYears(-filters.MinAge); //20 [04]
        var minBirthdate = currentDate.AddYears(-filters.MaxAge); //19 [79]


        query = query.Where(u => u.DateOfBirth >= minBirthdate && u.DateOfBirth <= maxBirthdate);


        query = filters.OrderBy switch
        {
            "created" => query.OrderByDescending(u => u.Created),
            _ => query.OrderByDescending(u => u.LastActive)
        };

      var source =   query.Include(u => u.Photos)
            .ProjectToType<MembersResponse>()
            .AsNoTracking();



        var paginatedMembers = await PaginatedList<MembersResponse>
                                   .CreateAsync(source, filters.PageNumber, filters.PageSize,cancellationToken);

        //var expirationTime = DateTimeOffset.Now.AddMinutes(30); // Cache for 30 min

        //_cacheService.SetData(cacheKey, paginatedMembers.Items, expirationTime);

        return Result.Success(paginatedMembers);
    }
    
    
    
    public async Task<Result<MemberResponse>> GetUserByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var user = await _context.Users.Where(u => u.Id == id)
                             .Include(u => u.Photos)
                            .ProjectToType<MemberResponse>()
                            .FirstOrDefaultAsync(cancellationToken);
                           

        if (user is null)
            return Result.Failure<MemberResponse>(UserError.UserNotFound);



        return Result.Success(user);

    }

}
