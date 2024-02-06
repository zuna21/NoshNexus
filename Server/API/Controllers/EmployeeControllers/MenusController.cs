using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;

using EmployeeQueryParams = ApplicationCore.QueryParams.EmployeeQueryParams;
using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using ApplicationCore;

namespace API.Controllers.EmployeeControllers;

[Authorize]
public class MenusController : DefaultEmployeeController
{
    private readonly IMenuService _menuService;
    public MenusController(
        IMenuService menuService
    )
    {
        _menuService = menuService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<int>> Create(EmployeeDtos.CreateMenuDto createMenuDto)
    {
        var response = await _menuService.EmployeeCreate(createMenuDto);
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
    public async Task<ActionResult<int>> Update(int id, EmployeeDtos.EditMenuDto employeeEditMenuDto)
    {
        var response = await _menuService.EmployeeUpdate(id, employeeEditMenuDto);
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
        var response = await _menuService.EmployeeDelete(id);
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

    [HttpGet("get-menus")]
    public async Task<ActionResult<PagedList<OwnerDtos.MenuCardDto>>> GetMenus([FromQuery] EmployeeQueryParams.MenusQueryParams menusQueryParams)
    {
        var response = await _menuService.GetEmployeeMenuCardDtos(menusQueryParams);
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

    [HttpGet("get-menu-edit/{id}")]
    public async Task<ActionResult<EmployeeDtos.GetMenuEditDto>> GetMenuEdit(int id)
    {
        var response = await _menuService.GetEmployeeMenuEdit(id);
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

    [HttpGet("get-menu/{id}")]
    public async Task<ActionResult<OwnerDtos.GetMenuDetailsDto>> GetMenu(int id, [FromQuery] OwnerQueryParams.MenuItemsQueryParams menuItemsQueryParams)
    {
        var response = await _menuService.GetEmployeeMenuDetails(id, menuItemsQueryParams);
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
