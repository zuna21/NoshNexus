namespace API;

public interface IRestaurantImageService
{
    Task<Response<ChangeProfileImageDto>> UploadProfileImage(int restaurantId, IFormFile image);
    Task<Response<ICollection<ImageDto>>> UploadImages(int restaurantId, IFormFileCollection images);
    Task<Response<bool>> Delete(int restaurantId, int imageId);
}
