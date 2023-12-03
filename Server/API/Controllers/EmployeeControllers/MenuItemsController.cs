using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.EmployeeControllers;

[Authorize]
public class MenuItemsController : DefaultEmployeeController
{
    private readonly IMenuItemService _menuItemService;
    public MenuItemsController(
        IMenuItemService menuItemService
    )
    {
        _menuItemService = menuItemService;
    }

    [HttpPost("create/{id}")]
    public async Task<ActionResult<MenuItemCardDto>> Create(int id, CreateMenuItemDto createMenuItemDto)
    {
        var response = await _menuItemService.EmployeeCreate(id, createMenuItemDto);
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

    [HttpPut("update/{id}")]
    public async Task<ActionResult<int>> Update(int id, EditMenuItemDto editMenuItemDto)
    {
        var response = await _menuItemService.EmployeeUpdate(id, editMenuItemDto);
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

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        var response = await _menuItemService.EmployeeDelete(id);
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

    [HttpGet("get-menu-item-edit/{id}")]
    public async Task<ActionResult<GetMenuItemEditDto>> GetMenuItemEdit(int id)
    {
        var response = await _menuItemService.GetEmployeeMenuItemEdit(id);
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

    [HttpGet("get-menu-item/{id}")]
    public async Task<ActionResult<MenuItemDetailsDto>> GetMenuItem(int id)
    {
        var response = await _menuItemService.GetEmployeeMenuItem(id);
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
