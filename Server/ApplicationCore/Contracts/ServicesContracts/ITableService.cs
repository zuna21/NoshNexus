using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface ITableService
{
    Task<Response<bool>> CreateTables(ICollection<OwnerDtos.TableCardDto> tableCardDtos);
    Task<Response<bool>> Delete(int tableId);
    Task<Response<PagedList<OwnerDtos.TableCardDto>>> GetTables(OwnerQueryParams.TablesQueryParams tablesQueryParams);
    Task<Response<ICollection<OwnerDtos.TableDto>>> GetAllRestaurantTableNames(int restaurantId);


    // Employee
    Task<Response<bool>> EmployeeCreate(ICollection<OwnerDtos.TableCardDto> tableCardDtos);
    Task<Response<ICollection<OwnerDtos.TableCardDto>>> GetEmployeeTables();
    Task<Response<bool>> EmployeeDelete(int tableId);



    // Customer
    Task<Response<ICollection<CustomerDtos.TableDto>>> GetRestaurantTables(int restaurantId);

}
