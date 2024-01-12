using ApplicationCore;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;

namespace API;

public class ChartService(
    IChartRepository chartRepository,
    IUserService userService
) : IChartService
{
    private readonly IChartRepository _chartRepository = chartRepository;
    private readonly IUserService _userService = userService;

    public async Task<Response<ICollection<int>>> GetOrdersByDay(int restaurantId, OrdersByDayQueryParams ordersByDayQueryParams)
    {
        Response<ICollection<int>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _chartRepository.GetOrdersByDay(restaurantId, owner.Id, ordersByDayQueryParams);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<PieChartDto>> GetTopTenMenuItems(int restaurantId, TopTenMenuOrdersQueryParams topTenMenuOrdersQueryParams)
    {
        Response<PieChartDto> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null) 
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _chartRepository.GetTopTenMenuItems(restaurantId, owner.Id, topTenMenuOrdersQueryParams);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }
}
