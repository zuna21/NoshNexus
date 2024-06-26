﻿using ApplicationCore.Entities;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using EmployeeQueryParams = ApplicationCore.QueryParams.EmployeeQueryParams;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IMenuRepository
{
    void AddMenu(Menu menu);
    Task<PagedList<OwnerDtos.MenuCardDto>> GetMenus(int ownerId, OwnerQueryParams.MenusQueryParams menusQueryParams);
    Task<OwnerDtos.GetMenuDetailsDto> GetMenu(int menuId, int ownerId, OwnerQueryParams.MenuItemsQueryParams menuItemsQueryParams);
    Task<OwnerDtos.GetMenuEditDto> GetMenuEdit(int menuId, int ownerId);
    Task<ICollection<OwnerDtos.GetRestaurantMenusForSelectDto>> GetRestaurantMenusForSelect(int restaurantId, int ownerId);
    Task<bool> SaveAllAsync();


    // Employee
    Task<PagedList<OwnerDtos.MenuCardDto>> GetEmployeeMenuCardDtos(int restaurantId, EmployeeQueryParams.MenusQueryParams menusQueryParams);
    Task<OwnerDtos.GetMenuDetailsDto> GetEmployeeMenu(int menuId, int restaurantId, OwnerQueryParams.MenuItemsQueryParams menuItemsQueryParams);
    Task<EmployeeDtos.GetMenuEditDto> GetEmployeeMenuEdit(int menuId, int restaurantId);


    // Customer
    Task<ICollection<CustomerDtos.MenuCardDto>> GetCustomerRestaurantMenus(int restaurantId, CustomerQueryParams.MenusQueryParams menusQueryParams);
    Task<CustomerDtos.MenuDto> GetCustomerMenu(int menuId);


    // For global
    Task<Menu> GetOwnerMenu(int menuId, int ownerId);
    Task<Menu> GetEmployeeMenuEntity(int menuId, int restaurantId);
}