﻿using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

[Authorize]
public class RestaurantsController(
    IRestaurantService restaurantService
    ) : DefaultCustomerController
{

    private readonly IRestaurantService _restaurantService = restaurantService;

    [HttpGet("get-restaurants")]
    public async Task<ActionResult<ICollection<RestaurantCardDto>>> GetRestaurants() 
    {
        var response = await _restaurantService.GetCustomerRestaurants();
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpGet("get-restaurant/{id}")]
    public async Task<ActionResult<CustomerRestaurantDetailsDto>> GetRestaurant(int id)
    {
        var response = await _restaurantService.GetCustomerRestaurant(id);
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