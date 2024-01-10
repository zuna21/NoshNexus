using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CustomerControllers;

public class MenuItemsController(IMenuItemService menuItemService) : DefaultCustomerController
{
    private readonly IMenuItemService _menuItemService = menuItemService;

    [HttpGet("get-best-menu-items/{restaurantId}")]
    public async Task<ActionResult<ICollection<CustomerMenuItemCardDto>>> GetBestMenuItems(int restaurantId)
    {
        var response = await _menuItemService.GetCustomerBestMenuItems(restaurantId);
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

}
