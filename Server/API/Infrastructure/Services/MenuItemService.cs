
namespace API;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMenuService _menuService;
    public MenuItemService(
        IMenuItemRepository menuItemRepository,
        IMenuService menuService
    )
    {
        _menuItemRepository = menuItemRepository;
        _menuService = menuService;
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
}
