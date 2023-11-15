using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<int>> Create(CreateMenuDto createMenuDto)
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


    [HttpGet("get-menus")]
    public async Task<ActionResult<ICollection<MenuCardDto>>> GetMenus()
    {
        var response = await _menuService.GetMenus();
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
        var response = await _menuService.GetMenu(id);
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
    public async Task<ActionResult<GetMenuEditDto>> GetMenuEdit(int id)
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
}
