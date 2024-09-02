

namespace Affection.Application.Interfaces
{
    public interface IImageUploadService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
        Task<DeletionResult> DeleteImageAsync(string publicId);
    }
}
