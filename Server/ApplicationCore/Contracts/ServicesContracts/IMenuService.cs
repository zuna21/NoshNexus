using ApplicationCore.DTOs;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParmas = ApplicationCore.QueryParams.CustomerQueryParams;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IMenuService
{
    Task<Response<int>> Create(CreateMenuDto createMenuDto);
    Task<Response<int>> Update(int menuId, EditMenuDto editMenuDto);
    Task<Response<int>> Delete(int menuId);
    Task<Response<PagedList<MenuCardDto>>> GetMenus(MenusQueryParams menusQueryParams);
    Task<Response<MenuDetailsDto>> GetMenu(int menuId, MenuItemsQueryParams menuItemsQueryParams);
    Task<Response<GetMenuEditDto>> GetMenuEdit(int menuId);
    Task<Response<ICollection<GetRestaurantMenusForSelectDto>>> GetRestaurantMenusForSelect(int restaurantId);


    // Employee
    Task<Response<ICollection<MenuCardDto>>> GetEmployeeMenuCardDtos();
    Task<Response<MenuDetailsDto>> GetEmployeeMenuDetails(int id);
    Task<Response<int>> EmployeeCreate(CreateMenuDto createMenuDto);
    Task<Response<GetEmployeeMenuEditDto>> GetEmployeeMenuEdit(int menuId);
    Task<Response<int>> EmployeeUpdate(int menuId, EmployeeEditMenuDto employeeEditMenuDto);
    Task<Response<int>> EmployeeDelete(int menuId);


    // Customer
    Task<Response<ICollection<CustomerDtos.MenuCardDto>>> GetCustomerRestaurantMenus(int restaurantId, CustomerQueryParmas.MenusQueryParams menusQueryParams);
    Task<Response<CustomerDtos.MenuDto>> GetCustomerMenu(int menuId);

}