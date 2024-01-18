using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface ITableService
{
    Task<Response<bool>> CreateTables(ICollection<OwnerDtos.TableCardDto> tableCardDtos);
    Task<Response<bool>> Delete(int tableId);
    Task<Response<PagedList<OwnerDtos.TableCardDto>>> GetTables(TablesQueryParams tablesQueryParams);


    // Employee
    Task<Response<bool>> EmployeeCreate(ICollection<OwnerDtos.TableCardDto> tableCardDtos);
    Task<Response<ICollection<OwnerDtos.TableCardDto>>> GetEmployeeTables();
    Task<Response<bool>> EmployeeDelete(int tableId);

    // Customer
    Task<Response<ICollection<OwnerDtos.GetRestaurantTableDto>>> GetRestaurantTables(int restaurantId);

}
