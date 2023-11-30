using ApplicationCore.DTOs;

namespace API;

public interface IRestaurantService
{
    Task<Response<int>> Create(CreateRestaurantDto createRestaurantDto);
    Task<Response<bool>> Update(int restaurantId, RestaurantEditDto restaurantEditDto);
    Task<Response<int>> Delete(int restaurantId);    
    Task<Response<ICollection<RestaurantCardDto>>> GetRestaurants();
    Task<Response<RestaurantDetailsDto>> GetRestaurant(int restaurantId);
    Task<Response<ICollection<RestaurantSelectDto>>> GetRestaurantSelect();
    Task<Response<GetRestaurantEditDto>> GetRestaurantEdit(int restaurantId);
    Task<Response<GetCreateRestaurantDto>> GetCreateRestaurant();


    // Funkcije globalne (nisu za kontrolor)
    Task<Restaurant> GetOwnerRestaurant(int restaurantId);
    Task<Restaurant> GetAnyRestaurantById(int restaurantId);
    Task<ICollection<RestaurantSelectDto>> GetRestaurantsForSelect();
}
