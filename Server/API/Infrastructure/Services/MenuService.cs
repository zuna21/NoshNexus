

namespace API;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IRestaurantService _restaurantService;
    private readonly IOwnerService _ownerService;
    public MenuService(
        IMenuRepository menuRepository,
        IRestaurantService restaurantService,
        IOwnerService ownerService
    )
    {
        _menuRepository = menuRepository;
        _restaurantService = restaurantService;
        _ownerService = ownerService;
    }
    public async Task<Response<string>> Create(CreateMenuDto createMenuDto)
    {
        Response<string> response = new();
        try
        {
            var restaurant = await _restaurantService.GetOwnerRestaurantById(createMenuDto.RestaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = new Menu
            {
                Name = createMenuDto.Name,
                Description = createMenuDto.Description,
                IsActive = createMenuDto.IsActive,
                RestaurantId = restaurant.Id,
                Restaurant = restaurant
            };

            _menuRepository.AddMenu(menu);
            if (!await _menuRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create menu.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = "Successfully created menu";
        }
        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<MenuDetailsDto>> GetMenuDetails(int menuId)
    {
        Response<MenuDetailsDto> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = await _menuRepository.GetMenuDetails(menuId, owner.Id);
            if (menu == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menu;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<GetMenuEditDto>> GetMenuEdit(int menuId)
    {
        Response<GetMenuEditDto> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = await _menuRepository.GetMenuEdit(menuId, owner.Id);
            if (menu == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var ownerRestaurants = await _restaurantService.GetRestaurantsForSelect();
            menu.OwnerRestaurants = ownerRestaurants;

            response.Status = ResponseStatus.Success;
            response.Data = menu;

        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<ICollection<MenuCardDto>>> GetOwnerMenus()
    {
        Response<ICollection<MenuCardDto>> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menus = await _menuRepository.GetOwnerMenus(owner.Id);

            response.Status = ResponseStatus.Success;
            response.Data = menus;
        }
        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }
}
