﻿using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;

using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace API.Controllers.CustomerControllers;

public class RestaurantsController(
    IRestaurantService restaurantService
    ) : DefaultCustomerController
{

    private readonly IRestaurantService _restaurantService = restaurantService;

    [HttpGet("get-restaurants")]
    public async Task<ActionResult<ICollection<OwnerDtos.RestaurantCardDto>>> GetRestaurants([FromQuery] CustomerQueryParams.RestaurantsQueryParams restaurantsQueryParams)
    {
        var response = await _restaurantService.GetCustomerRestaurants(restaurantsQueryParams);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpGet("get-restaurant/{restaurantId}")]
    public async Task<ActionResult<CustomerDtos.RestaurantDto>> GetRestaurant(int restaurantId)
    {
        var response = await _restaurantService.GetCustomerRestaurant(restaurantId);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }

}
