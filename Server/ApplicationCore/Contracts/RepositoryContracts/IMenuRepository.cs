using ApplicationCore.DTOs;
using ApplicationCore.Entities;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IMenuRepository
{
    void AddMenu(Menu menu);
    Task<PagedList<OwnerDtos.MenuCardDto>> GetMenus(int ownerId, MenusQueryParams menusQueryParams);
    Task<OwnerDtos.GetMenuDetailsDto> GetMenu(int menuId, int ownerId, MenuItemsQueryParams menuItemsQueryParams);
    Task<OwnerDtos.GetMenuEditDto> GetMenuEdit(int menuId, int ownerId);
    Task<ICollection<OwnerDtos.GetRestaurantMenusForSelectDto>> GetRestaurantMenusForSelect(int restaurantId, int ownerId);
    Task<bool> SaveAllAsync();


    // Employee
    Task<ICollection<OwnerDtos.MenuCardDto>> GetEmployeeMenuCardDtos(int restaurantId);
    Task<OwnerDtos.GetMenuDetailsDto> GetEmployeeMenu(int menuId, int restaurantId);
    Task<EmployeeDtos.GetMenuEditDto> GetEmployeeMenuEdit(int menuId, int restaurantId);


    // Customer
    Task<ICollection<CustomerDtos.MenuCardDto>> GetCustomerRestaurantMenus(int restaurantId, CustomerQueryParams.MenusQueryParams menusQueryParams);
    Task<CustomerDtos.MenuDto> GetCustomerMenu(int menuId);


    // For global
    Task<Menu> GetOwnerMenu(int menuId, int ownerId);
    Task<Menu> GetEmployeeMenuEntity(int menuId, int restaurantId);
}