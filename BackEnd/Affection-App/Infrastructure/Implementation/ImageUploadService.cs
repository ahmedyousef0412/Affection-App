
namespace Affection.Infrastructure.Implementation;

public class ImageUploadService : IImageUploadService
{
    private readonly Cloudinary _cloudinary;
    private readonly List<string> _allowedExtension = [".jpg", ".jpeg", ".png"];
    private readonly int _allowedMaxLength = 2097152;


    public ImageUploadService(IOptions<CloudinarySettings> cloudinary)
    {
        Account account = new()
        {
            Cloud = cloudinary.Value.Cloud,
            ApiKey = cloudinary.Value.ApiKey,
            ApiSecret = cloudinary.Value.ApiSecret,
        };

        _cloudinary = new Cloudinary(account);
    }



    public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
    {
        var uploadResult = new ImageUploadResult();

        if (file.Length > _allowedMaxLength)
        {
            uploadResult.Error.Message = ImageError.MaxLength;
            return uploadResult;
        }

        if (!IsValidExtension(file.FileName))
        {
            uploadResult.Error.Message = ImageError.ValidExtension;
            return uploadResult;
        }


        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation()
                               .Width(500)
                               .Height(500)
                               .Crop("fill")
                               .Gravity(Gravity.Face)
        };

        uploadResult =  await _cloudinary.UploadAsync(uploadParams);

        return uploadResult;


    }


    public async Task<DeletionResult> DeleteImageAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);

        var result = await _cloudinary.DestroyAsync(deleteParams);

        return result;
    }



    private bool IsValidExtension(string fileName)
    {
        var extension = Path.GetExtension(fileName).ToLower();
        return _allowedExtension.Contains(extension);
    }
}
