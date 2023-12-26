using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IMenuService
{
    Task<Response<int>> Create(CreateMenuDto createMenuDto);
    Task<Response<int>> Update(int menuId, EditMenuDto editMenuDto);
    Task<Response<int>> Delete(int menuId);
    Task<Response<ICollection<MenuCardDto>>> GetMenus();
    Task<Response<MenuDetailsDto>> GetMenu(int menuId);
    Task<Response<GetMenuEditDto>> GetMenuEdit(int menuId);


    // Employee
    Task<Response<ICollection<MenuCardDto>>> GetEmployeeMenuCardDtos();
    Task<Response<MenuDetailsDto>> GetEmployeeMenuDetails(int id);
    Task<Response<int>> EmployeeCreate(CreateMenuDto createMenuDto);
    Task<Response<GetEmployeeMenuEditDto>> GetEmployeeMenuEdit(int menuId);
    Task<Response<int>> EmployeeUpdate(int menuId, EmployeeEditMenuDto employeeEditMenuDto);
    Task<Response<int>> EmployeeDelete(int menuId);


    // Customer
    Task<Response<ICollection<CustomerMenuCardDto>>> GetCustomerRestaurantMenus(int restaurantId, string sq);
    Task<Response<CustomerMenuDetailsDto>> GetCustomerMenu(int menuId);

}