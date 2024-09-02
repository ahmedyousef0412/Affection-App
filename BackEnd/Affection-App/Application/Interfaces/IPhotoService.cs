
namespace Affection.Application.Interfaces;
public interface IPhotoService
{
    Task<Result> UploadPhotoAsync(IFormFile file, string UserId, CancellationToken cancellationToken = default);
    Task<Result> DeletePhotoAsync(string userId, int photoId, CancellationToken cancellationToken = default);
    Task<Result> SetPhotoMainAsync(string userId, int photoId, CancellationToken cancellationToken = default);

}
