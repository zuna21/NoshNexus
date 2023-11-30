using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface ITableService
{
    Task<Response<bool>> CreateTables(ICollection<TableCardDto> tableCardDtos);
    Task<Response<bool>> Delete(int tableId);
    Task<Response<ICollection<TableCardDto>>> GetTables();


    // blobal functions
    Task<Table> GetRestaurantTable(int tableId, int restaurantId);
}
