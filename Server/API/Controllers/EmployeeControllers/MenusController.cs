using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<int>> Create(CreateMenuDto createMenuDto)
    {
        var response = await _menuService.EmployeeCreate(createMenuDto);
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
    public async Task<ActionResult<ICollection<MenuCardDto>>> GetMenus()
    {
        var response = await _menuService.GetEmployeeMenuCardDtos();
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
    public async Task<ActionResult<MenuDetailsDto>> GetMenu(int id)
    {
        var response = await _menuService.GetEmployeeMenuDetails(id);
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
