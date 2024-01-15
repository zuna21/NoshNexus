using ApplicationCore.Contracts.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using ApplicationCore.DTOs;

namespace API.Controllers.CustomerControllers;

public class MenusController(IMenuService menuService) : DefaultCustomerController
{
    private readonly IMenuService _menuService = menuService;

    [HttpGet("get-restaurant-menus/{restaurantId}")]
    public async Task<ActionResult<ICollection<CustomerDtos.MenuCardDto>>> GetRestaurantMenus(int restaurantId)
    {
        var response = await _menuService.GetCustomerRestaurantMenus(restaurantId);
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
