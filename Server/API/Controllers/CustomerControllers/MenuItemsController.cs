using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

[Authorize]
public class MenuItemsController(IMenuItemService menuItemService) : DefaultCustomerController
{
    private readonly IMenuItemService _menuItemService = menuItemService;

    [HttpGet("get-restaurant-menu-items/{restaurantId}")]
    public async Task<ActionResult<ICollection<MenuItemRowDto>>> GetRestaurantMenuItems(int restaurantId, string sq = "")
    {
        var response = await _menuItemService.GetCustomerRestaurantMenuItems(restaurantId, sq);
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

    [HttpGet("get-menu-menu-items/{menuId}")]
    public async Task<ActionResult<ICollection<MenuItemRowDto>>> GetMenuMenuItems(int menuId, string sq = "")
    {
        var response = await _menuItemService.GetCustomerMenuMenuItems(menuId, sq);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest("Something went wrong.");
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
