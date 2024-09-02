

namespace Affection.Contract.Photos;
public class DeletePhotoRequestValidator:AbstractValidator<DeletePhotoRequest>
{
    public DeletePhotoRequestValidator()
    {
        RuleFor(x => x.PublicId)
          .NotEmpty().WithMessage("Public ID is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");
    }
}
