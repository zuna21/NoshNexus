﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class MenuItemsController : DefaultOwnerController
{
    private readonly IMenuItemService _menuItemService;
    private readonly IMenuItemImageService _menuItemImageService;
    public MenuItemsController(
        IMenuItemService menuItemService,
        IMenuItemImageService menuItemImageService
    )
    {
        _menuItemService = menuItemService;
        _menuItemImageService = menuItemImageService;
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

    [HttpPut("update/{id}")]
    public async Task<ActionResult<int>> Update(int id, EditMenuItemDto editMenuItemDto)
    {
        var response = await _menuItemService.Update(id, editMenuItemDto);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<int>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<int>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<int>)response.Data,
            _ => (ActionResult<int>)BadRequest("Something went wrong."),
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

    [HttpPost("upload-profile-image/{id}")]
    public async Task<ActionResult<ImageDto>> UploadProfileImage(int id)
    {
        var image = Request.Form.Files[0];
        var response = await _menuItemImageService.UploadProfileImage(id, image);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<ImageDto>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<ImageDto>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<ImageDto>)response.Data,
            _ => (ActionResult<ImageDto>)BadRequest("Something went wrong"),
        };
    }

    [HttpDelete("delete-image/{id}")]
    public async Task<ActionResult<int>> DeleteImage(int id)
    {
        var response = await _menuItemImageService.DeleteImage(id);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<int>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<int>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<int>)response.Data,
            _ => (ActionResult<int>)BadRequest("Something went wrong"),
        };
    }

}
