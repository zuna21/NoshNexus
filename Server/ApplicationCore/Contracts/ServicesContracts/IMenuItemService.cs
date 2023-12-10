using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IMenuItemService
{
    Task<Response<MenuItemCardDto>> Create(int menuId, CreateMenuItemDto createMenuItemDto); 
    Task<Response<int>> Update(int menuItemId, EditMenuItemDto editMenuItemDto);
    Task<Response<MenuItemDetailsDto>> GetMenuItem(int menuItemId);
    Task<Response<GetMenuItemEditDto>> GetMenuItemEdit(int menuItemId);
    Task<Response<int>> Delete(int menuItemId);
    
    // Employee
    Task<Response<MenuItemDetailsDto>> GetEmployeeMenuItem(int menuItemId);
    Task<Response<MenuItemCardDto>> EmployeeCreate(int menuId, CreateMenuItemDto createMenuItemDto);
    Task<Response<GetMenuItemEditDto>> GetEmployeeMenuItemEdit(int menuItemId);
    Task<Response<int>> EmployeeUpdate(int menuItemId, EditMenuItemDto editMenuItemDto);
    Task<Response<int>> EmployeeDelete(int menuItemId);

    // Customer
    Task<Response<ICollection<MenuItemRowDto>>> GetCustomerRestaurantMenuItems(int restaurantId);
    

    // Globalne funkcije
    Task<MenuItem> GetOwnerMenuItem(int menuItemId);
    Task<ICollection<MenuItem>> GetRestaurantMenuItems(ICollection<int> menuItemIds, int restauranId);


}
