
namespace API;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IOwnerService _ownerService;
    private readonly IRestaurantService _restaurantService;
    public TableService(
        ITableRepository tableRepository,
        IOwnerService ownerService,
        IRestaurantService restaurantService
    )
    {
        _tableRepository = tableRepository;
        _ownerService = ownerService;
        _restaurantService = restaurantService;
    }
    public async Task<Response<bool>> CreateTables(ICollection<TableCardDto> tableCardDtos)
    {
        Response<bool> response = new();
        try
        {
            List<Restaurant> restaurants = new();
            foreach (var tableDto in tableCardDtos)
            {
                if (!restaurants.Select(x => x.Id).Contains(tableDto.Restaurant.Id))
                {
                    var restaurant = await _restaurantService.GetOwnerRestaurant(tableDto.Restaurant.Id);
                    if (restaurant == null)
                    {
                        response.Status = ResponseStatus.NotFound;
                        return response;
                    }
                    restaurants.Add(restaurant);
                }
                var table = new Table
                {
                    Name = tableDto.Name,
                    RestaurantId = tableDto.Restaurant.Id,
                    Restaurant = restaurants.FirstOrDefault(x => x.Id == tableDto.Restaurant.Id)
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
            response.Data = true;
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

            var tables = await _tableRepository.GetTables(owner.Id);
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
