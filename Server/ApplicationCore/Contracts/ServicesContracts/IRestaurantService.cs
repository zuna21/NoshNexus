using ApplicationCore.DTOs;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IRestaurantService
{
    Task<Response<int>> Create(CreateRestaurantDto createRestaurantDto);
    Task<Response<bool>> Update(int restaurantId, RestaurantEditDto restaurantEditDto);
    Task<Response<int>> Delete(int restaurantId);    
    Task<Response<ICollection<RestaurantCardDto>>> GetRestaurants(RestaurantsQueryParams restaurantsQueryParams);
    Task<Response<RestaurantDetailsDto>> GetRestaurant(int restaurantId);
    Task<Response<ICollection<RestaurantSelectDto>>> GetRestaurantSelect();
    Task<Response<GetRestaurantEditDto>> GetRestaurantEdit(int restaurantId);
    Task<Response<GetCreateRestaurantDto>> GetCreateRestaurant();



    // Employee
    Task<Response<RestaurantDetailsDto>> GetEmployeeRestaurantDetailsDto();



    // Customer
    Task<Response<ICollection<RestaurantCardDto>>> GetCustomerRestaurants(string sq);
    Task<Response<CustomerRestaurantDetailsDto>> GetCustomerRestaurant(int restaurantId);

}
