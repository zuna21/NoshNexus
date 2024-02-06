using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using CustomerQueryParmas = ApplicationCore.QueryParams.CustomerQueryParams;
using EmployeeQueryParams = ApplicationCore.QueryParams.EmployeeQueryParams;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IMenuService
{
    Task<Response<int>> Create(OwnerDtos.CreateMenuDto createMenuDto);
    Task<Response<int>> Update(int menuId, OwnerDtos.EditMenuDto editMenuDto);
    Task<Response<int>> Delete(int menuId);
    Task<Response<PagedList<OwnerDtos.MenuCardDto>>> GetMenus(OwnerQueryParams.MenusQueryParams menusQueryParams);
    Task<Response<OwnerDtos.GetMenuDetailsDto>> GetMenu(int menuId, OwnerQueryParams.MenuItemsQueryParams menuItemsQueryParams);
    Task<Response<OwnerDtos.GetMenuEditDto>> GetMenuEdit(int menuId);
    Task<Response<ICollection<OwnerDtos.GetRestaurantMenusForSelectDto>>> GetRestaurantMenusForSelect(int restaurantId);


    // Employee
    Task<Response<PagedList<OwnerDtos.MenuCardDto>>> GetEmployeeMenuCardDtos(EmployeeQueryParams.MenusQueryParams menusQueryParams);
    Task<Response<OwnerDtos.GetMenuDetailsDto>> GetEmployeeMenuDetails(int id);
    Task<Response<EmployeeDtos.GetMenuEditDto>> GetEmployeeMenuEdit(int menuId);
    Task<Response<int>> EmployeeCreate(EmployeeDtos.CreateMenuDto createMenuDto);
    Task<Response<int>> EmployeeUpdate(int menuId, EmployeeDtos.EditMenuDto employeeEditMenuDto);
    Task<Response<int>> EmployeeDelete(int menuId);


    // Customer
    Task<Response<ICollection<CustomerDtos.MenuCardDto>>> GetCustomerRestaurantMenus(int restaurantId, CustomerQueryParmas.MenusQueryParams menusQueryParams);
    Task<Response<CustomerDtos.MenuDto>> GetCustomerMenu(int menuId);

}