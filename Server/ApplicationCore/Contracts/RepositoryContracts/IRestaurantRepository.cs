using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IRestaurantRepository
{
    void Create(Restaurant restaurant);
    Task<ICollection<RestaurantCardDto>> GetRestaurants(int ownerId, RestaurantsQueryParams restaurantsQueryParams);
    Task<RestaurantDetailsDto> GetRestaurant(int restaurantId, int ownerId);
    Task<ICollection<RestaurantSelectDto>> GetRestaurantSelect(int ownerId);
    Task<GetRestaurantEditDto> GetRestaurantEdit(int restaurantId, int ownerId);
    Task<bool> SaveAllAsync();



    // Employee
    Task<RestaurantDetailsDto> GetEmployeeRestaurantDetailsDto(int restaurantId);


    // Customer
    Task<ICollection<RestaurantCardDto>> GetCustomerRestaurants();


    // For Hubs
    Restaurant GetRestaurantByIdSync(int restaurantId);


    // For Global
    Task<Restaurant> GetOwnerRestaurant(int restaurantId, int ownerId);
    Task<Restaurant> GetAnyRestaurantById(int restaurantId);
}
