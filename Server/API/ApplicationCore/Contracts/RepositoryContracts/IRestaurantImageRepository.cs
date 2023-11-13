namespace API;

public interface IRestaurantImageRepository
{
    void AddImage(RestaurantImage image);
    Task<RestaurantImage> GetProfileImage(int restaurantId);
    Task<ICollection<ImageDto>> GetRestaurantGalleryImages(int restaurantId);
    Task<bool> SaveAllAsync();
}
