namespace API;

public interface IAppUserImageService
{
    Task<Response<ImageDto>> UploadProfileImage(IFormFile image);

    Task<ImageDto> GetUserProfileImage();
}
