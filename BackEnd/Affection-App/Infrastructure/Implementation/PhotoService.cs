
using Affection.Domain.Entities;
using System.Threading;

namespace Affection.Infrastructure.Implementation;

public class PhotoService(IImageUploadService imageUploadService, 
    UserManager<ApplicationUser> userManager,
    ApplicationDbContext context) : IPhotoService
{
    private readonly IImageUploadService _imageUploadService = imageUploadService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly ApplicationDbContext _context = context;

    public async Task<Result> UploadPhotoAsync(IFormFile file, string UserId, CancellationToken cancellationToken = default)
    {
      
        var user = await _context.Users
                               .Where(u => u.Id == UserId)
                               .Include(u => u.Photos)
                               .SingleOrDefaultAsync(cancellationToken);


        var uploadImageResult = await _imageUploadService.UploadImageAsync(file);

        if (uploadImageResult.Error is not null)
            return Result.Failure(ImageError.PhotoNotAdded);
        

        var photo = new Photo
        {
            Url = uploadImageResult.SecureUrl.AbsoluteUri,
            PublicId = uploadImageResult.PublicId
        };


        if (user!.Photos.Count == 0)
            photo.IsMain = true;
        
        user.Photos.Add(photo);

  
        await _context.SaveChangesAsync(cancellationToken);

      
        return Result.Success();
    }

    public async Task<Result> DeletePhotoAsync(string userId,  int photoId, CancellationToken cancellationToken = default)
    {
        
        var (user ,photo) = await GetUserAndPhoto(userId, photoId, cancellationToken);

                              
        if (photo is null)
            return Result.Failure(ImageError.PhotoNotFound);


        if (photo.IsMain)
            return Result.Failure(ImageError.FailedToDeleteMainPhoto);


        if (photo.PublicId is not null)
        {
            var deleteResult = await _imageUploadService.DeleteImageAsync(photo.PublicId);

            if (deleteResult.Error is not null)
                return Result.Failure(ImageError.FailedToDeletePhoto);

        }

        _context.Photos.Remove(photo);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> SetPhotoMainAsync(string userId, int photoId, CancellationToken cancellationToken = default)
    {
        var (user, photo) = await GetUserAndPhoto(userId, photoId, cancellationToken);


        if (photo is null)
            return Result.Failure(ImageError.PhotoNotFound);


        if (photo.IsMain)
            return Result.Failure(ImageError.PhotoIsMain);


        var currentMainPhoto = user!.Photos.FirstOrDefault(p => p.IsMain);

        if (currentMainPhoto is not null)
            currentMainPhoto.IsMain = false;

        photo.IsMain = true;


        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }



    private async Task<Photo> User(string userId,int photoId ,CancellationToken cancellationToken)
    {
       var user =  await _context.Users
                             .Where(u => u.Id == userId)
                             .Include(u => u.Photos)
                             .SingleOrDefaultAsync(cancellationToken);


      var photo = user!.Photos?
                .FirstOrDefault(p => p.Id == photoId);


        return photo;

    }

    private async Task<(ApplicationUser user, Photo photo)> GetUserAndPhoto(string userId,int photoId ,CancellationToken cancellationToken)
    {
       

        var user = await _context.Users
                            .Where(u => u.Id == userId)
                            .Include(u => u.Photos)
                            .SingleOrDefaultAsync(cancellationToken);


        var photo = user!.Photos.FirstOrDefault(p => p.Id == photoId);
                              

        return (user, photo);
    }
}


