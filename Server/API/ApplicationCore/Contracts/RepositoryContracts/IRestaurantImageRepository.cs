namespace API;

public interface IRestaurantImageRepository
{
    void AddImage(RestaurantImage image);
    Task<RestaurantImage> GetProfileImage(int restaurantId);
    Task<RestaurantImage> GetImage(int restaurantId, int imageId);
    Task<ICollection<ImageDto>> GetRestaurantGalleryImages(int restaurantId);
    Task<bool> SaveAllAsync();
}
