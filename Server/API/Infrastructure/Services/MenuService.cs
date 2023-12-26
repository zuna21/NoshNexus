

using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IUserService _userService;
    private readonly IRestaurantRepository _restaurantRepository;
    public MenuService(
        IMenuRepository menuRepository,
        IUserService userService,
        IRestaurantRepository restaurantRepository
    )
    {
        _menuRepository = menuRepository;
        _userService = userService;
        _restaurantRepository = restaurantRepository;
    }
    public async Task<Response<int>> Create(CreateMenuDto createMenuDto)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            var restaurant = await _restaurantRepository.GetOwnerRestaurant(createMenuDto.RestaurantId, owner.Id);
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
            var owner = await _userService.GetOwner();
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
            var owner = await _userService.GetOwner();
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


    public async Task<Response<ICollection<MenuCardDto>>> GetMenus()
    {
        Response<ICollection<MenuCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
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
            var owner = await _userService.GetOwner();
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
                var restaurant = await _restaurantRepository.GetOwnerRestaurant(editMenuDto.RestaurantId, owner.Id);
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

    public async Task<Response<int>> Delete(int menuId)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
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

            menu.IsDeleted = true;

            if (!await _menuRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete menu.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menu.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<MenuCardDto>>> GetEmployeeMenuCardDtos()
    {
        Response<ICollection<MenuCardDto>> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menus = await _menuRepository.GetEmployeeMenuCardDtos(employee.RestaurantId);
            if (menus == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menus;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<MenuDetailsDto>> GetEmployeeMenuDetails(int id)
    {
        Response<MenuDetailsDto> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = await _menuRepository.GetEmployeeMenu(id, employee.RestaurantId);
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
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<int>> EmployeeCreate(CreateMenuDto createMenuDto)
    {
        Response<int> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var restaurant = await _restaurantRepository.GetAnyRestaurantById(employee.RestaurantId);
            if (restaurant == null) 
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = new Menu
            {
                Name = createMenuDto.Name,
                Description = createMenuDto.Description,
                RestaurantId = restaurant.Id,
                Restaurant = restaurant,
                IsActive = createMenuDto.IsActive
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
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<GetEmployeeMenuEditDto>> GetEmployeeMenuEdit(int menuId)
    {
        Response<GetEmployeeMenuEditDto> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = await _menuRepository.GetEmployeeMenuEdit(menuId, employee.RestaurantId);
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
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<int>> EmployeeUpdate(int menuId, EmployeeEditMenuDto employeeEditMenuDto)
    {
        Response<int> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = await _menuRepository.GetEmployeeMenuEntity(menuId, employee.RestaurantId);
            if (menu == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            menu.IsActive = employeeEditMenuDto.IsActive;
            menu.Name = employeeEditMenuDto.Name;
            menu.Description = employeeEditMenuDto.Description;

            if (!await _menuRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Change at least one property.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menu.Id;

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<int>> EmployeeDelete(int menuId)
    {
        Response<int> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = await _menuRepository.GetEmployeeMenuEntity(menuId, employee.RestaurantId);
            if (menu == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            menu.IsDeleted = true;
            if (!await _menuRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Something went wrong.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menu.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<CustomerMenuCardDto>>> GetCustomerRestaurantMenus(int restaurantId, string sq)
    {
        Response<ICollection<CustomerMenuCardDto>> response = new();
        try
        {
            response.Status = ResponseStatus.Success;
            response.Data = await _menuRepository.GetCustomerRestaurantMenus(restaurantId, sq);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<CustomerMenuDetailsDto>> GetCustomerMenu(int menuId)
    {
        Response<CustomerMenuDetailsDto> response = new();
        try
        {
            response.Status = ResponseStatus.Success;
            response.Data = await _menuRepository.GetCustomerMenu(menuId);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }
}
