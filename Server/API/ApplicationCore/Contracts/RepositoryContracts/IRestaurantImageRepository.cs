namespace API;

public interface IRestaurantImageRepository
{
    void AddImage(RestaurantImage image);
    Task<bool> SaveAllAsync();
}
