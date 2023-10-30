namespace API;

public interface ITableRepository
{
    void AddTable(Table table);
    Task<ICollection<TableCardDto>> GetTables(Owner owner);
    Task<bool> SaveAllAsync();
}
