namespace API;

public interface IMenuItemImageService
{
    Task<Response<ImageDto>> UploadProfileImage(int menuItemId, IFormFile image);
    Task<Response<int>> DeleteImage(int imageId);
}
