
namespace API;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IOwnerService _ownerService;
    public TableService(
        ITableRepository tableRepository,
        IOwnerService ownerService
    )
    {
        _tableRepository = tableRepository;
        _ownerService = ownerService;
    }
    public async Task<Response<string>> CreateTables(ICollection<TableCardDto> tableCardDtos)
    {
        Response<string> response = new();
        try
        {
            foreach (var tableDto in tableCardDtos)
            {
                var table = new Table
                {
                    Name = tableDto.Name,
                    RestaurantId = tableDto.Restaurant.Id
                };
                _tableRepository.AddTable(table);
            }

            if (!await _tableRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create tables";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = "Successfully created tables.";
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }
        return response;
    }

    public async Task<Response<ICollection<TableCardDto>>> GetTables()
    {
        Response<ICollection<TableCardDto>> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var tables = await _tableRepository.GetTables(owner);
            if (tables == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = tables;
        }
        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }
}
