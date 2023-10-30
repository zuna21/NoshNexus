namespace API;

public interface IRestaurantRepository
{
    void Create(Restaurant restaurant);
    Task<bool> SaveAllAsync();
    Task<ICollection<RestaurantCardDto>> GetOwnerRestaurants(Owner owner);
}
