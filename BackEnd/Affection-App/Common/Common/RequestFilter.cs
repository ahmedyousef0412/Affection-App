
namespace Affection.Contract.Common;
public record RequestFilter
{


    private const int MaxPageSize = 50;

    private int _pageSize = 5;

    public int PageNumber { get; init; } = 1;


    public int PageSize
    {

        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;

    }


    public string UserId { get; set; } = string.Empty;
    public string? Gender { get; set; }
    public string OrderBy { get; set; } = "LastActive";
    public int MinAge { get; set; } = 18;
    public int MaxAge { get; set; } = 60;
}

