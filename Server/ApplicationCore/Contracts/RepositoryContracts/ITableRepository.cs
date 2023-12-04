using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface ITableRepository
{
    void AddTable(Table table);
    void AddMany(ICollection<Table> tables);
    void Delete(Table table);
    Task<ICollection<TableCardDto>> GetTables(int ownerId);
    Task<bool> SaveAllAsync();


    // Employees
    Task<ICollection<TableCardDto>> GetEmployeeTables(int restaurantId);


    // Global functions
    Task<Table> GetOwnerTable(int tableId, int ownerId);
    Task<Table> GetRestaurantTable(int tableId, int restaurantId);
}
