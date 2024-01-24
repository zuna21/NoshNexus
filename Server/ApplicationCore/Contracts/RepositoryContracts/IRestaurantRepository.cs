using ApplicationCore.Entities;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IRestaurantRepository
{
    void Create(Restaurant restaurant);
    Task<ICollection<OwnerDtos.RestaurantCardDto>> GetRestaurants(int ownerId, OwnerQueryParams.RestaurantsQueryParams restaurantsQueryParams);
    Task<OwnerDtos.GetRestaurantDetailsDto> GetRestaurant(int restaurantId, int ownerId);
    Task<ICollection<OwnerDtos.GetRestaurantForSelectDto>> GetRestaurantSelect(int ownerId);
    Task<OwnerDtos.GetRestaurantEditDto> GetRestaurantEdit(int restaurantId, int ownerId);
    Task<bool> SaveAllAsync();



    // Employee
    Task<OwnerDtos.GetRestaurantDetailsDto> GetEmployeeRestaurantDetailsDto(int restaurantId);


    // Customer
    Task<ICollection<OwnerDtos.RestaurantCardDto>> GetCustomerRestaurants(CustomerQueryParams.RestaurantsQueryParams restaurantsQueryParams);
    Task<CustomerDtos.RestaurantDto> GetCustomerRestaurant(int restaurantId, int customerId);
    Task<FavouriteCustomerRestaurant> GetFavouriteCustomerRestaurant(int customerId, int restaurantId);
    void AddFavouriteRestaurant(FavouriteCustomerRestaurant favouriteCustomerRestaurant);
    void RemoveFavouriteRestaurant(FavouriteCustomerRestaurant favouriteCustomerRestaurant);


    // For Hubs
    Restaurant GetRestaurantByIdSync(int restaurantId);


    // For Global
    Task<Restaurant> GetOwnerRestaurant(int restaurantId, int ownerId);
    Task<Restaurant> GetAnyRestaurantById(int restaurantId);


    // For hubs new (Can use async)
    Task<ICollection<string>> GetOwnerRestaurantNames(int ownerId);
}
