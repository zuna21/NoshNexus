namespace API;

public interface IRestaurantService
{
    Task<Response<string>> Create(CreateRestaurantDto createRestaurantDto);
    Task<Response<ICollection<RestaurantCardDto>>> GetOwnerRestaurants();
    Task<Response<RestaurantDetailsDto>> GetRestaurantDetails(int restaurantId);
    Task<Response<ICollection<RestaurantSelectDto>>> GetRestaurantSelect();
    Task<Response<GetRestaurantEditDto>> GetRestaurantEdit(int restaurantId);
}
