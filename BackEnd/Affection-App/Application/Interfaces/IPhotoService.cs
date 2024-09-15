
using Affection.Contract.Photos;

namespace Affection.Application.Interfaces;
public interface IPhotoService
{
    Task<Result> UploadPhotoAsync(IFormFile file, string UserId, CancellationToken cancellationToken = default);
    Task<Result> DeletePhotoAsync(string userId, int photoId, CancellationToken cancellationToken = default);
    Task<Result> SetPhotoMainAsync(string userId, int photoId, CancellationToken cancellationToken = default);

    Task<IEnumerable<PhotoResponse>> GetPhotos(string userId ,CancellationToken cancellation = default);
    Task<IEnumerable<MemberPhotosResponse>> GetPhotosByUserId(string userId ,CancellationToken cancellation = default);

}
