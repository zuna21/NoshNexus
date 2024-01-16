using ApplicationCore.Contracts.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;

namespace API.Controllers.CustomerControllers;

public class MenusController(IMenuService menuService) : DefaultCustomerController
{
    private readonly IMenuService _menuService = menuService;

    [HttpGet("get-restaurant-menus/{restaurantId}")]
    public async Task<ActionResult<ICollection<CustomerDtos.MenuCardDto>>> GetRestaurantMenus(int restaurantId, [FromQuery] CustomerQueryParams.MenusQueryParams menusQueryParams)
    {
        var response = await _menuService.GetCustomerRestaurantMenus(restaurantId, menusQueryParams);
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

    [HttpGet("get-menu/{menuId}")]
    public async Task<ActionResult<CustomerDtos.MenuDto>> GetMenu(int menuId)
    {
        var response = await _menuService.GetCustomerMenu(menuId);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong");
        }
    }

}
