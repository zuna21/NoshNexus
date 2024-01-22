using ApplicationCore.DTOs;
using ApplicationCore.Entities;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IMenuItemRepository
{
    void AddMenuItem(MenuItem menuItem);
    Task<OwnerDtos.GetMenuItemDetailsDto> GetMenuItem(int menuItemId, int ownerId);
    Task<OwnerDtos.GetMenuItemEditDto> GetMenuItemEdit(int menuItemId, int ownerId);
    Task<bool> SaveAllAsync();

    // Employee 
    Task<OwnerDtos.GetMenuItemDetailsDto> GetEmployeeMenuItem(int menuItemId, int restaurantId);
    Task<OwnerDtos.GetMenuItemEditDto> GetEmployeeMenuItemEdit(int menuItemId, int restaurantId);


    // Customer
    Task<ICollection<CustomerDtos.MenuItemCardDto>> GetCustomerRestaurantMenuItems(int restaurantId, CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams);
    Task<ICollection<CustomerDtos.MenuItemCardDto>> GetCustomerMenuMenuItems(int menuId, CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams);
    void AddFavouriteMenuItem(FavouriteCustomerMenuItem favouriteCustomerMenuItem);


    
    // Global Functions
    Task<MenuItem> GetOwnerMenuItem(int menuItemId, int ownerId);
    Task<MenuItem> GetRestaurantMenuItemEntity(int restaurantId, int menuItemId);
    Task<MenuItem> GetAnyMenuItemById(int menuItemId);

}
