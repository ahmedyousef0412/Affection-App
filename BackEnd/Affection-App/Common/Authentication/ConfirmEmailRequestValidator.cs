
namespace Affection.Contract.Authentication;
public class ConfirmEmailRequestValidator:AbstractValidator<ConfirmEmailRequest>
{
    public ConfirmEmailRequestValidator()
    {
        RuleFor(cer => cer.UserId).NotEmpty();

        RuleFor(cer => cer.Code).NotEmpty();
    }
}
