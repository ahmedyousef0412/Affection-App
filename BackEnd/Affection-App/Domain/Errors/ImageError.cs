
namespace Affection.Domain.Errors;
public static class ImageError
{
    public const string MaxLength = "File Can't be more than 2MB";
    public const string ValidExtension = "Invalid file extension. Only JPG, JPEG, and PNG are allowed.";
   
    public static readonly Error PhotoNotFound =
        new("Photo.PhotoNotFound", "Photo not found", StatusCodes.Status400BadRequest);
  
    public static readonly Error PhotoNotAdded =
        new("Photo.FailedAddPhoto", "Failed to add photo", StatusCodes.Status400BadRequest);
   
    public static readonly Error PhotoIsMain =
        new("Photo.PhotoIsMain", "Photo is already main!", StatusCodes.Status400BadRequest);
   

  
    public static readonly Error FailedToDeleteMainPhoto =
        new("Photo.Can'tDeleteMainPhoto", "You cannot delete  main photo!", StatusCodes.Status400BadRequest);
    
    public static readonly Error FailedToDeletePhoto = 
        new("Photo.FailedToDeletePhoto", "Failed to delete photo", StatusCodes.Status400BadRequest);
}

