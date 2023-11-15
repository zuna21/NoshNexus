

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
    public async Task<Response<int>> Create(CreateMenuDto createMenuDto)
    {
        Response<int> response = new();
        try
        {
            var restaurant = await _restaurantService.GetOwnerRestaurant(createMenuDto.RestaurantId);
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
            response.Data = menu.Id;
        }
        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<MenuDetailsDto>> GetMenu(int menuId)
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

            var menu = await _menuRepository.GetMenu(menuId, owner.Id);
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

    public async Task<Menu> GetOwnerMenu(int menuId)
    {
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null) return null;
            return await _menuRepository.GetOwnerMenu(menuId, owner.Id);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return null;
    }

    public async Task<Response<ICollection<MenuCardDto>>> GetMenus()
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

            var menus = await _menuRepository.GetMenus(owner.Id);

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

    public async Task<Response<int>> Update(int menuId, EditMenuDto editMenuDto)
    {
        Response<int> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = await _menuRepository.GetOwnerMenu(menuId, owner.Id);
            if (menu == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (menu.RestaurantId != editMenuDto.RestaurantId)
            {
                var restaurant = await _restaurantService.GetOwnerRestaurant(editMenuDto.RestaurantId);
                if (restaurant == null)
                {
                    response.Status = ResponseStatus.NotFound;
                    return response;
                }

                menu.RestaurantId = restaurant.Id;
                menu.Restaurant = restaurant;
            }

            menu.Name = editMenuDto.Name;
            menu.Description = editMenuDto.Description;
            menu.IsActive = editMenuDto.IsActive;

            if (!await _menuRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update restaurant.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menu.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
        }

        return response;
    }
}
