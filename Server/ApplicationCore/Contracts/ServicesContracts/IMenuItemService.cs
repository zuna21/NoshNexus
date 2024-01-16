﻿using ApplicationCore.DTOs;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;

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
    Task<Response<ICollection<CustomerDtos.MenuItemCardDto>>> GetCustomerRestaurantMenuItems(int restaurantId, CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams);
    Task<Response<ICollection<CustomerDtos.MenuItemCardDto>>> GetCustomerMenuMenuItems(int menuId, CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams);

}
