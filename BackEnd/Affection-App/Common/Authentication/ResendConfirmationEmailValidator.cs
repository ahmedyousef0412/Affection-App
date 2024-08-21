

namespace Affection.Contract.Authentication;
public class ResendConfirmationEmailValidator:AbstractValidator<ResendConfirmationEmail>
{
    public ResendConfirmationEmailValidator()
    {
        RuleFor(rce =>rce.Email).NotEmpty();
    }
}
