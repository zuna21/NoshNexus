using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface ITableService
{
    Task<Response<bool>> CreateTables(ICollection<TableCardDto> tableCardDtos);
    Task<Response<bool>> Delete(int tableId);
    Task<Response<ICollection<TableCardDto>>> GetTables();


    // Employee
    Task<Response<bool>> EmployeeCreate(ICollection<TableCardDto> tableCardDtos);
    Task<Response<ICollection<TableCardDto>>> GetEmployeeTables();
    Task<Response<bool>> EmployeeDelete(int tableId);

    // Customer
    Task<Response<ICollection<TableRestaurant>>> GetRestaurantTables(int restaurantId);

    // blobal functions
    Task<Table> GetRestaurantTable(int tableId, int restaurantId);
}
