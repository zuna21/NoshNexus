using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class MenuItemsController : DefaultOwnerController
{
    private readonly IMenuItemService _menuItemService;
    public MenuItemsController(
        IMenuItemService menuItemService
    )
    {
        _menuItemService = menuItemService;
    }


    [HttpPost("create/{id}")]
    public async Task<ActionResult<MenuItemCardDto>> Create(int id, CreateMenuItemDto createMenuItemDto) // id je od menu
    {
        var response = await _menuItemService.Create(id, createMenuItemDto);
        return response.Status switch
        {
            ResponseStatus.BadRequest => (ActionResult<MenuItemCardDto>)BadRequest(response.Message),
            ResponseStatus.NotFound => (ActionResult<MenuItemCardDto>)NotFound(),
            ResponseStatus.Success => (ActionResult<MenuItemCardDto>)response.Data,
            _ => (ActionResult<MenuItemCardDto>)BadRequest("Something went wrong"),
        };
    }

    [HttpGet("get-menu-item/{id}")]
    public async Task<ActionResult<MenuItemDetailsDto>> GetMenuItem(int id)
    {
        var result = await _menuItemService.GetMenuItem(id);
        switch (result.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(result.Message);
            case ResponseStatus.Success:
                return result.Data;
            default:
                return BadRequest("Something went wrong");
        }
    }

    [HttpGet("get-menu-item-edit/{id}")]
    public async Task<ActionResult<GetMenuItemEditDto>> GetMenuItemEdit(int id)
    {
        var response = await _menuItemService.GetMenuItemEdit(id);
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
