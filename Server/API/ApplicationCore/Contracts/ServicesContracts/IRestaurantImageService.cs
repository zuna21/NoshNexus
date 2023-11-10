namespace API;

public interface IRestaurantImageService
{
    Task<Response<ImageDto>> UploadProfileImage(int restaurantId, IFormFile image);
}
