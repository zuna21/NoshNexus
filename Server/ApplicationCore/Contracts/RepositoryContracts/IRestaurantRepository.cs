using ApplicationCore.Entities;

using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IRestaurantRepository
{
    void Create(Restaurant restaurant);
    Task<ICollection<OwnerDtos.RestaurantCardDto>> GetRestaurants(int ownerId, RestaurantsQueryParams restaurantsQueryParams);
    Task<OwnerDtos.GetRestaurantDetailsDto> GetRestaurant(int restaurantId, int ownerId);
    Task<ICollection<OwnerDtos.GetRestaurantForSelectDto>> GetRestaurantSelect(int ownerId);
    Task<OwnerDtos.GetRestaurantEditDto> GetRestaurantEdit(int restaurantId, int ownerId);
    Task<bool> SaveAllAsync();



    // Employee
    Task<OwnerDtos.GetRestaurantDetailsDto> GetEmployeeRestaurantDetailsDto(int restaurantId);


    // Customer
    Task<ICollection<OwnerDtos.RestaurantCardDto>> GetCustomerRestaurants(CustomerQueryParams.RestaurantsQueryParams restaurantsQueryParams);
    Task<CustomerDtos.RestaurantDto> GetCustomerRestaurant(int restaurantId);


    // For Hubs
    Restaurant GetRestaurantByIdSync(int restaurantId);


    // For Global
    Task<Restaurant> GetOwnerRestaurant(int restaurantId, int ownerId);
    Task<Restaurant> GetAnyRestaurantById(int restaurantId);
}
