using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.EmployeeControllers;

public class RestaurantsController : DefaultEmployeeController
{
    private readonly IRestaurantService _restaurantService;
    public RestaurantsController(
        IRestaurantService restaurantService
    )
    {
        _restaurantService = restaurantService;
    }

    [HttpGet("get-restaurant")]
    public async Task<ActionResult<RestaurantDetailsDto>> GetRestaurant()
    {
        var response = await _restaurantService.GetEmployeeRestaurantDetailsDto();
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
