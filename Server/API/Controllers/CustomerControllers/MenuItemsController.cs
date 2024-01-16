using ApplicationCore.Contracts.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;

namespace API.Controllers.CustomerControllers;

public class MenuItemsController(IMenuItemService menuItemService) : DefaultCustomerController
{
    private readonly IMenuItemService _menuItemService = menuItemService;

    [HttpGet("get-restaurant-menu-items/{restaurantId}")]
    public async Task<ActionResult<ICollection<CustomerDtos.MenuItemCardDto>>> GetRestaurantMenuItems(int restaurantId, [FromQuery] CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams)
    {
        var response = await _menuItemService.GetCustomerRestaurantMenuItems(restaurantId, menuItemsQueryParams);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
