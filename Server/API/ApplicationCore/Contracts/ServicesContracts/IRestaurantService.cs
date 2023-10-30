namespace API;

public interface IRestaurantService
{
    Task<Response<string>> Create(CreateRestaurantDto createRestaurantDto);
}
