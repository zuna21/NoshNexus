using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class RestaurantsController : DefaultOwnerController
{
    private readonly IRestaurantService _restaurantService;
    public RestaurantsController(
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

    [HttpGet("get-restaurants")]
    public async Task<ActionResult<ICollection<RestaurantCardDto>>> GetRestaurants()
    {
        Response<ICollection<RestaurantCardDto>> response = await _restaurantService.GetOwnerRestaurants();
        switch (response.Status)
        {
            case ResponseStatus.Unauthorized:
                return Unauthorized(response.Message);
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong");
        }
    }

    [HttpGet("get-restaurant-details/{id}")]
    public async Task<ActionResult<RestaurantDetailsDto>> GetRestaurantDetails(int id)
    {
        Response<RestaurantDetailsDto> response = await _restaurantService.GetRestaurantDetails(id);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong");
        }
    }

    [HttpGet("get-restaurants-for-select")]
    public async Task<ActionResult<ICollection<RestaurantSelectDto>>> GetRestaurantsForSelect()
    {
        Response<ICollection<RestaurantSelectDto>> response = await _restaurantService.GetRestaurantSelect();
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest: 
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong");
        }
    }
}
