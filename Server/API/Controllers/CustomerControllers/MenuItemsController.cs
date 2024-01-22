using ApplicationCore.Contracts.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using Microsoft.AspNetCore.Authorization;

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

    [HttpGet("get-menu-menu-items/{menuId}")]
    public async Task<ActionResult<CustomerDtos.MenuItemCardDto>> GetMenuMenuItems(int menuId, [FromQuery] CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams)
    {
        var response = await _menuItemService.GetCustomerMenuMenuItems(menuId, menuItemsQueryParams);
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

    [Authorize]
    [HttpGet("add-favourite-menu-item/{menuItemId}")]
    public async Task<ActionResult<bool>> AddFavouriteMenuItem(int menuItemId)
    {
        var response = await _menuItemService.AddFavouriteMenuItem(menuItemId);
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
