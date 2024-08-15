

using Affection.Domain.Abstraction;
using Microsoft.AspNetCore.Http;

namespace Affection.Domain.Errors;
public static class UserError
{
    public static readonly Error DuplicatedEmail =
     new("User.DuplicatedEmail", "Email is already exist", StatusCodes.Status409Conflict);

    public static readonly Error InvalidCredentials =
        new("User.InvalidCredentials", "Invalid Email or Password", StatusCodes.Status401Unauthorized);


    public static readonly Error RestrictedUser =
           new("User.RestrictedUser", "Restricted User , please contact with your administrator", StatusCodes.Status401Unauthorized);
   
    public static readonly Error EmailNotConfirmed =
       new("User.EmailNotConfirmed", "Email is not confirmed ", StatusCodes.Status401Unauthorized);
}
