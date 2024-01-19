using ApplicationCore.Entities;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface ITableRepository
{
    void AddTable(Table table);
    void AddMany(ICollection<Table> tables);
    void Delete(Table table);
    Task<PagedList<OwnerDtos.TableCardDto>> GetTables(int ownerId, OwnerQueryParams.TablesQueryParams tablesQueryParams);
    Task<bool> SaveAllAsync();


    // Employees
    Task<ICollection<OwnerDtos.TableCardDto>> GetEmployeeTables(int restaurantId);

    // Customer
    Task<ICollection<CustomerDtos.TableDto>> GetRestaurantTables(int restaurantId);


    // Global functions
    Task<Table> GetOwnerTable(int tableId, int ownerId);
    Task<Table> GetRestaurantTable(int tableId, int restaurantId);
}
