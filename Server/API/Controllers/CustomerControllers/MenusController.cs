using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

public class MenusController(IMenuService menuService) : DefaultCustomerController
{
    private readonly IMenuService _menuService = menuService;

    [HttpGet("get-menus/{restaurantId}")]
    public async Task<ActionResult<ICollection<CustomerMenuCardDto>>> GetMenus(int restaurantId)
    {
        var response = await _menuService.GetCustomerRestaurantMenus(restaurantId);
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

    [HttpGet("get-menu/{menuId}")]
    public async Task<ActionResult<CustomerMenuDetailsDto>> GetMenu(int menuId)
    {
        var response = await _menuService.GetCustomerMenu(menuId);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
