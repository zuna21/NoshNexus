using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IRestaurantRepository
{
    void Create(Restaurant restaurant);
    void BlockCustomer(RestaurantBlockedCustomers restaurantBlockedCustomers);
    Task<ICollection<RestaurantCardDto>> GetRestaurants(int ownerId, RestaurantsQueryParams restaurantsQueryParams);
    Task<RestaurantDetailsDto> GetRestaurant(int restaurantId, int ownerId);
    Task<ICollection<RestaurantSelectDto>> GetRestaurantSelect(int ownerId);
    Task<GetRestaurantEditDto> GetRestaurantEdit(int restaurantId, int ownerId);
    Task<bool> SaveAllAsync();



    // Employee
    Task<RestaurantDetailsDto> GetEmployeeRestaurantDetailsDto(int restaurantId);


    // Customer
    Task<ICollection<RestaurantCardDto>> GetCustomerRestaurants(string sq);
    Task<CustomerRestaurantDetailsDto> GetCustomerRestaurant(int restaurantId);


    // For Hubs
    Restaurant GetRestaurantByIdSync(int restaurantId);


    // For Global
    Task<Restaurant> GetOwnerRestaurant(int restaurantId, int ownerId);
    Task<Restaurant> GetAnyRestaurantById(int restaurantId);
}
