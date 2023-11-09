namespace API;

public interface IRestaurantService
{
    Task<Response<int>> Create(CreateRestaurantDto createRestaurantDto);
    Task<Response<ICollection<RestaurantCardDto>>> GetRestaurants();
    Task<Response<RestaurantDetailsDto>> GetRestaurant(int restaurantId);
    Task<Response<ICollection<RestaurantSelectDto>>> GetRestaurantSelect();
    Task<Response<GetRestaurantEditDto>> GetRestaurantEdit(int restaurantId);
    Task<Response<GetCreateRestaurantDto>> GetCreateRestaurant();

    Task<Response<bool>> UploadImages(int restaurantId, IFormFileCollection images);

    // Funkcije globalne (nisu za kontrolor)
    Task<Restaurant> GetOwnerRestaurant(int restaurantId);
    Task<ICollection<RestaurantSelectDto>> GetRestaurantsForSelect();
}
