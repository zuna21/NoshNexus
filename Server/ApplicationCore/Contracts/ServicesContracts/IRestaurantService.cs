using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IRestaurantService
{
    Task<Response<int>> Create(OwnerDtos.CreateRestaurantDto createRestaurantDto);
    Task<Response<bool>> Update(int restaurantId, OwnerDtos.EditRestaurantDto restaurantEditDto);
    Task<Response<int>> Delete(int restaurantId);    
    Task<Response<ICollection<OwnerDtos.RestaurantCardDto>>> GetRestaurants(OwnerQueryParams.RestaurantsQueryParams restaurantsQueryParams);
    Task<Response<OwnerDtos.GetRestaurantDetailsDto>> GetRestaurant(int restaurantId);
    Task<Response<ICollection<OwnerDtos.GetRestaurantForSelectDto>>> GetRestaurantSelect();
    Task<Response<OwnerDtos.GetRestaurantEditDto>> GetRestaurantEdit(int restaurantId);
    Task<Response<OwnerDtos.GetCreateRestaurantDto>> GetCreateRestaurant();



    // Employee
    Task<Response<OwnerDtos.GetRestaurantDetailsDto>> GetEmployeeRestaurantDetailsDto();




    // Customer
    Task<Response<ICollection<OwnerDtos.RestaurantCardDto>>> GetCustomerRestaurants(CustomerQueryParams.RestaurantsQueryParams restaurantsQueryParams);
    Task<Response<CustomerDtos.RestaurantDto>> GetCustomerRestaurant(int restaurantId);
    Task<Response<ICollection<OwnerDtos.RestaurantCardDto>>> GetCustomerFavouriteRestaurants();
    Task<Response<bool>> AddFavouriteRestaurant(int restaurantId);
    Task<Response<int>> RemoveFavouriteRestaurant(int restaurantId);
}
