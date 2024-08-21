
namespace Affection.Contract.Authentication;
public class ForgetPasswordRequestValidator:AbstractValidator<ForgetPasswordRequest>
{
    public ForgetPasswordRequestValidator()
    {
        RuleFor(fp => fp.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
