﻿using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult> Create(int id, CreateMenuItemDto createMenuItemDto) // id je od menu
    {
        var response = await _menuItemService.Create(id, createMenuItemDto);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong");
        }
    }
}
