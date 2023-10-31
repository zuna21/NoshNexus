
namespace API;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMenuService _menuService;
    private readonly IOwnerService _ownerService;
    public MenuItemService(
        IMenuItemRepository menuItemRepository,
        IMenuService menuService,
        IOwnerService ownerService
    )
    {
        _menuItemRepository = menuItemRepository;
        _menuService = menuService;
        _ownerService = ownerService;
    }
    public async Task<Response<string>> Create(int menuId, CreateMenuItemDto createMenuItemDto)
    {
        Response<string> response = new();
        try
        {
            var menu = await _menuService.GetOwnerMenuById(menuId);
            if (menu == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            var menuItem = new MenuItem
            {
                Name = createMenuItemDto.Name,
                Description = createMenuItemDto.Description,
                HasSpecialOffer = createMenuItemDto.HasSpecialOffer,
                IsActive = createMenuItemDto.IsActive,
                Price = createMenuItemDto.Price,
                SpecialOfferPrice = createMenuItemDto.SpecialOfferPrice,
                MenuId = menu.Id,
                Menu = menu
            };

            _menuItemRepository.AddMenuItem(menuItem);
            if (!await _menuItemRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create menu item.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = "Successfully created menu item";
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<MenuItemDetailsDto>> GetMenuItemDetails(int menuItemId)
    {
        Response<MenuItemDetailsDto> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menuItem = await _menuItemRepository.MenuItemDetails(menuItemId, owner.Id);
            if (menuItem == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menuItem;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }
}
