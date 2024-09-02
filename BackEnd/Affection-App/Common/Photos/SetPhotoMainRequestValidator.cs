

namespace Affection.Contract.Photos;
public class SetPhotoMainRequestValidator:AbstractValidator<SetPhotoMainRequest>
{
    public SetPhotoMainRequestValidator()
    {
        RuleFor(x => x.PhotoId)
     .NotEmpty().WithMessage("Phot oId  is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}
