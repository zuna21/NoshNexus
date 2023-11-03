namespace API;

public interface IRestaurantRepository
{
    void Create(Restaurant restaurant);
    Task<ICollection<RestaurantCardDto>> GetRestaurants(int ownerId);
    Task<Restaurant> GetRestaurantById(int restaurantId, int ownerId);
    Task<RestaurantDetailsDto> GetRestaurant(int restaurantId, int ownerId);
    Task<ICollection<RestaurantSelectDto>> GetRestaurantSelect(int ownerId);
    Task<GetRestaurantEditDto> GetRestaurantEdit(int restaurantId, int ownerId);
    Task<bool> SaveAllAsync();
}
