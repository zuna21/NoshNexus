using ApplicationCore.DTOs;
using ApplicationCore.Entities;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IMenuItemRepository
{
    void AddMenuItem(MenuItem menuItem);
    Task<MenuItemDetailsDto> GetMenuItem(int menuItemId, int ownerId);
    Task<GetMenuItemEditDto> GetMenuItemEdit(int menuItemId, int ownerId);
    Task<bool> SaveAllAsync();

    // Employee 
    Task<MenuItemDetailsDto> GetEmployeeMenuItem(int menuItemId, int restaurantId);
    Task<GetMenuItemEditDto> GetEmployeeMenuItemEdit(int menuItemId, int restaurantId);


    // Customer
    Task<ICollection<CustomerDtos.MenuItemCardDto>> GetCustomerRestaurantMenuItems(int restaurantId, CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams);

    
    // Global Functions
    Task<MenuItem> GetOwnerMenuItem(int menuItemId, int ownerId);
    Task<MenuItem> GetRestaurantMenuItemEntity(int restaurantId, int menuItemId);

}
