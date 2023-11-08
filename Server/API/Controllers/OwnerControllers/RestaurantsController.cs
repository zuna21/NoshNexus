﻿using Microsoft.AspNetCore.Authorization;
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
        Response<ICollection<RestaurantCardDto>> response = await _restaurantService.GetRestaurants();
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

    [HttpGet("get-restaurant/{id}")]
    public async Task<ActionResult<RestaurantDetailsDto>> GetRestaurant(int id)
    {
        Response<RestaurantDetailsDto> response = await _restaurantService.GetRestaurant(id);
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

    [HttpGet("get-restaurant-edit/{id}")]
    public async Task<ActionResult<GetRestaurantEditDto>> GetRestaurantEdit(int id)
    {
        var response = await _restaurantService.GetRestaurantEdit(id);
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

    [HttpGet("get-restaurant-create")]
    public async Task<ActionResult<GetCreateRestaurantDto>> GetRestaurantCreate()
    {
        var response = await _restaurantService.GetCreateRestaurant();
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
