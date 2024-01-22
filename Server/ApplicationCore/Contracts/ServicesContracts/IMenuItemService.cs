using ApplicationCore.DTOs;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IMenuItemService
{
    Task<Response<OwnerDtos.MenuItemCardDto>> Create(int menuId, OwnerDtos.CreateMenuItemDto createMenuItemDto); 
    Task<Response<int>> Update(int menuItemId, OwnerDtos.EditMenuItemDto editMenuItemDto);
    Task<Response<OwnerDtos.GetMenuItemDetailsDto>> GetMenuItem(int menuItemId);
    Task<Response<OwnerDtos.GetMenuItemEditDto>> GetMenuItemEdit(int menuItemId);
    Task<Response<int>> Delete(int menuItemId);




    
    // Employee
    Task<Response<OwnerDtos.GetMenuItemDetailsDto>> GetEmployeeMenuItem(int menuItemId);
    Task<Response<OwnerDtos.MenuItemCardDto>> EmployeeCreate(int menuId, OwnerDtos.CreateMenuItemDto createMenuItemDto);
    Task<Response<OwnerDtos.GetMenuItemEditDto>> GetEmployeeMenuItemEdit(int menuItemId);
    Task<Response<int>> EmployeeUpdate(int menuItemId, OwnerDtos.EditMenuItemDto editMenuItemDto);
    Task<Response<int>> EmployeeDelete(int menuItemId);





    // Customer
    Task<Response<ICollection<CustomerDtos.MenuItemCardDto>>> GetCustomerRestaurantMenuItems(int restaurantId, CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams);
    Task<Response<ICollection<CustomerDtos.MenuItemCardDto>>> GetCustomerMenuMenuItems(int menuId, CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams);
    Task<Response<bool>> AddFavouriteMenuItem(int menuItemId);
    Task<Response<int>> RemoveFavouriteMenuItem(int menuItemId);

}
