

namespace Affection.Domain.Errors;
public static  class MemberError
{

    public static readonly Error InvalidGender =
      new("Member.InvalidGender", "Invalid Gender", StatusCodes.Status400BadRequest);
}
