using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class RestaurantController : DefaultOwnerController
{
    private readonly IRestaurantService _restaurantService;
    public RestaurantController(
        IRestaurantService restaurantService
    )
    {
        _restaurantService = restaurantService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<string>> Create(CreateRestaurantDto createRestaurantDto)
    {
        Response<string> response = await _restaurantService.Create(createRestaurantDto);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Unauthorized:
                return Unauthorized(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }

    }
}
