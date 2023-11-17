namespace API;

public interface ITableService
{
    Task<Response<bool>> CreateTables(ICollection<TableCardDto> tableCardDtos);
    Task<Response<bool>> Delete(int tableId);
    Task<Response<ICollection<TableCardDto>>> GetTables();
}
