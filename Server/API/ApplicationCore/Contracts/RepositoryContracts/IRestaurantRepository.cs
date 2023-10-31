namespace API;

public interface IRestaurantRepository
{
    void Create(Restaurant restaurant);
    Task<bool> SaveAllAsync();
    Task<ICollection<RestaurantCardDto>> GetOwnerRestaurants(int ownerId);
    Task<Restaurant> GetOwnerRestaurantById(int restaurantId, int ownerId);
    Task<RestaurantDetailsDto> GetRestaurantDetails(int restaurantId, int ownerId);
    Task<ICollection<RestaurantSelectDto>> GetRestaurantSelect(int ownerId);
    Task<GetRestaurantEditDto> GetRestaurantEdit(int restaurantId, int ownerId);
}
