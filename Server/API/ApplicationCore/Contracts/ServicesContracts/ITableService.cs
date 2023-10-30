namespace API;

public interface ITableService
{
    Task<Response<string>> CreateTables(ICollection<TableCardDto> tableCardDtos);
    Task<Response<ICollection<TableCardDto>>> GetTables();
}
