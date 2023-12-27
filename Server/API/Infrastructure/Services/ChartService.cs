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

    public async Task<Response<ICollection<TopTenMenuItemsDto>>> GetTopTenMenuItems(int restaurantId)
    {
        Response<ICollection<TopTenMenuItemsDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _chartRepository.GetTopTenMenuItems(restaurantId);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<WeekDayOrdersDto>>> GetWeekDayOrders(int restaurantId)
    {
        Response<ICollection<WeekDayOrdersDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _chartRepository.GetWeekDayOrders(restaurantId);
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
