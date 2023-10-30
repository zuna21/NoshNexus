namespace API;

public interface IRestaurantRepository
{
    void Create(Restaurant restaurant);
    Task<bool> SaveAllAsync();
    Task<ICollection<RestaurantCardDto>> GetOwnerRestaurants(Owner owner);
    Task<Restaurant> GetOwnerRestaurantById(int restaurantId, Owner owner);
    Task<RestaurantDetailsDto> GetRestaurantDetails(int restaurantId, Owner owner);
    Task<ICollection<RestaurantSelectDto>> GetRestaurantSelect(Owner owner);
    Task<GetRestaurantEditDto> GetRestaurantEdit(int restaurantId, Owner owner);
}
