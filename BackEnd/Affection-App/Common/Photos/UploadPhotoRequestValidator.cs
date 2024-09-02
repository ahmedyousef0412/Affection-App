
namespace Affection.Contract.Photos;
public class UploadPhotoRequestValidator:AbstractValidator<UploadPhotoRequest>
{
    public UploadPhotoRequestValidator()
    {
        RuleFor(x => x.UserId)
       .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.File)
            .NotEmpty().WithMessage("Image file is required.")
            .Must(BeAValidImage).WithMessage("File must be an image.");
    }

    private bool BeAValidImage(IFormFile file)
    {
      
        var allowedFileExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return allowedFileExtensions.Contains(extension);
    }
}
