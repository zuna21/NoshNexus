namespace API;

public interface ITableRepository
{
    void AddTable(Table table);
    Task<ICollection<TableCardDto>> GetTables(int ownerId);
    Task<bool> SaveAllAsync();
}
