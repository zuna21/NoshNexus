using ApplicationCore.DTOs;
using ApplicationCore.Entities;

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
    Task<ICollection<MenuItemRowDto>> GetCustomerRestaurantMenuItems(int restaurantId, string sq);
    Task<ICollection<MenuItemRowDto>> GetCustomerMenuMenuItems(int menuId, string sq);

    
    // Global Functions
    Task<MenuItem> GetOwnerMenuItem(int menuItemId, int ownerId);
    Task<MenuItem> GetEmployeeMenuItemEntity(int menuItemId, int restaurantId);
    Task<ICollection<MenuItem>> GetRestaurantMenuItems(ICollection<int> menuItemIds, int restaurantId);




}
