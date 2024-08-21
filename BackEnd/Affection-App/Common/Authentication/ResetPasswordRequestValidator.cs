
namespace Affection.Contract.Authentication;
public class ResetPasswordRequestValidator:AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(u => u.Code)
         .NotEmpty();
      

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.NewPassword)
           .NotEmpty()
           .Matches(RegexPatterns.Password)
           .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric and Uppercase");


    }
}
