using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface ITableService
{
    Task<Response<bool>> CreateTables(ICollection<TableCardDto> tableCardDtos);
    Task<Response<bool>> Delete(int tableId);
    Task<Response<PagedList<TableCardDto>>> GetTables(TablesQueryParams tablesQueryParams);


    // Employee
    Task<Response<bool>> EmployeeCreate(ICollection<TableCardDto> tableCardDtos);
    Task<Response<ICollection<TableCardDto>>> GetEmployeeTables();
    Task<Response<bool>> EmployeeDelete(int tableId);

    // Customer
    Task<Response<ICollection<TableRestaurant>>> GetRestaurantTables(int restaurantId);

}
