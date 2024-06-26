﻿using ApplicationCore;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace API;

[Authorize]
public class MenusController : DefaultOwnerController
{
    private readonly IMenuService _menuService;
    public MenusController(
        IMenuService menuService
    )
    {
        _menuService = menuService;
    }


    [HttpPost("create")]
    public async Task<ActionResult<int>> Create(OwnerDtos.CreateMenuDto createMenuDto)
    {
        var response = await _menuService.Create(createMenuDto);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<int>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<int>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<int>)Ok(response.Data),
            _ => (ActionResult<int>)BadRequest("Something went wrong."),
        };
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<int>> Update(int id, OwnerDtos.EditMenuDto editMenuDto)
    {
        var response = await _menuService.Update(id, editMenuDto);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<int>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<int>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<int>)response.Data,
            _ => (ActionResult<int>)BadRequest("Something went wrong."),
        };
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        var response = await _menuService.Delete(id);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<int>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<int>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<int>)response.Data,
            _ => (ActionResult<int>)BadRequest("Something went wrong."),
        };
    }


    [HttpGet("get-menus")]
    public async Task<ActionResult<ICollection<OwnerDtos.MenuCardDto>>> GetMenus([FromQuery] OwnerQueryParams.MenusQueryParams menusQueryParams)
    {
        var response = await _menuService.GetMenus(menusQueryParams);
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

    [HttpGet("get-menu/{id}")]
    public async Task<ActionResult<OwnerDtos.GetMenuDetailsDto>> GetMenu(int id, [FromQuery] OwnerQueryParams.MenuItemsQueryParams menuItemsQueryParams)
    {
        var response = await _menuService.GetMenu(id, menuItemsQueryParams);
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


    [HttpGet("get-menu-edit/{id}")]
    public async Task<ActionResult<OwnerDtos.GetMenuEditDto>> GetMenuEdit(int id)
    {
        var response = await _menuService.GetMenuEdit(id);
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

    [HttpGet("get-restaurant-menus-for-select/{restaurantId}")]
    public async Task<ActionResult<ICollection<OwnerDtos.GetRestaurantMenusForSelectDto>>> GetRestaurantMenusForSelect(int restaurantId)
    {
        var response = await _menuService.GetRestaurantMenusForSelect(restaurantId);
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
