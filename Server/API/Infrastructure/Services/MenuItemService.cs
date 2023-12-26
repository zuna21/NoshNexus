using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IMenuService _menuService;
    private readonly IOwnerService _ownerService;
    private readonly IUserService _userService;
    public MenuItemService(
        IMenuItemRepository menuItemRepository,
        IMenuService menuService,
        IOwnerService ownerService,
        IUserService userService
    )
    {
        _menuItemRepository = menuItemRepository;
        _menuService = menuService;
        _ownerService = ownerService;
        _userService = userService;
    }
    public async Task<Response<MenuItemCardDto>> Create(int menuId, CreateMenuItemDto createMenuItemDto)
    {
        Response<MenuItemCardDto> response = new();
        try
        {
            var menu = await _menuService.GetOwnerMenu(menuId);
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
            response.Data = new MenuItemCardDto
            {
                Id = menuItem.Id,
                Description = menuItem.Description,
                HasSpecialOffer = menuItem.HasSpecialOffer,
                Image = "",
                IsActive = menuItem.IsActive,
                Name = menuItem.Name,
                Price = menuItem.Price,
                SpecialOfferPrice = menuItem.SpecialOfferPrice
            };
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<int>> Delete(int menuItemId)
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
            var menuItem = await _menuItemRepository.GetOwnerMenuItem(menuItemId, owner.Id);
            if (menuItem == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            menuItem.IsDeleted = true;
            if (!await _menuItemRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete menu item.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menuItem.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
        }

        return response;
    }

    public async Task<Response<MenuItemCardDto>> EmployeeCreate(int menuId, CreateMenuItemDto createMenuItemDto)
    {
        Response<MenuItemCardDto> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = await _menuService.GetEmployeeMenuEntity(menuId);
            if (menu == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            MenuItem menuItem = new()
            {
                Name = createMenuItemDto.Name,
                Description = createMenuItemDto.Description,
                Price = createMenuItemDto.Price,
                HasSpecialOffer = createMenuItemDto.HasSpecialOffer,
                IsActive = createMenuItemDto.IsActive,
                MenuId = menu.Id,
                Menu = menu,
                SpecialOfferPrice = createMenuItemDto.SpecialOfferPrice,
            };

            _menuItemRepository.AddMenuItem(menuItem);
            if (!await _menuItemRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create menu item.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = new MenuItemCardDto
            {
                Id = menuItem.Id,
                Description = menuItem.Description,
                HasSpecialOffer = menuItem.HasSpecialOffer,
                Image = "",
                IsActive = menuItem.IsActive,
                Name = menuItem.Name,
                Price = menuItem.Price,
                SpecialOfferPrice = menuItem.SpecialOfferPrice
            };

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            return response;
        }

        return response;
    }

    public async Task<Response<int>> EmployeeDelete(int menuItemId)
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

            var menuItem = await _menuItemRepository.GetRestaurantMenuItemEntity(employee.RestaurantId, menuItemId);
            if (menuItem == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            menuItem.IsDeleted = true;
            if (!await _menuItemRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete menu item.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menuItem.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<int>> EmployeeUpdate(int menuItemId, EditMenuItemDto editMenuItemDto)
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

            var menuItem = await _menuItemRepository.GetRestaurantMenuItemEntity(employee.RestaurantId, menuItemId);
            if (menuItem == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            menuItem.Name = editMenuItemDto.Name;
            menuItem.Price = editMenuItemDto.Price;
            menuItem.Description = editMenuItemDto.Description;
            menuItem.IsActive = editMenuItemDto.IsActive;
            menuItem.HasSpecialOffer = editMenuItemDto.HasSpecialOffer;
            menuItem.SpecialOfferPrice = editMenuItemDto.SpecialOfferPrice;

            if (!await _menuItemRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update menu item.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menuItem.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<MenuItemRowDto>>> GetCustomerMenuMenuItems(int menuId, string sq)
    {
        Response<ICollection<MenuItemRowDto>> response = new();
        try
        {
            var menuItems = await _menuItemRepository.GetCustomerMenuMenuItems(menuId, sq);
            response.Status = ResponseStatus.Success;
            response.Data = menuItems;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<MenuItemRowDto>>> GetCustomerRestaurantMenuItems(int restaurantId, string sq)
    {
        Response<ICollection<MenuItemRowDto>> response = new();
        try
        {
            var menuItems = await _menuItemRepository.GetCustomerRestaurantMenuItems(restaurantId, sq);
            response.Status = ResponseStatus.Success;
            response.Data = menuItems;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<MenuItemDetailsDto>> GetEmployeeMenuItem(int menuItemId)
    {
        Response<MenuItemDetailsDto> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menuItem = await _menuItemRepository.GetEmployeeMenuItem(menuItemId, employee.RestaurantId);
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
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<GetMenuItemEditDto>> GetEmployeeMenuItemEdit(int menuItemId)
    {
        Response<GetMenuItemEditDto> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menuItem = await _menuItemRepository.GetEmployeeMenuItemEdit(menuItemId, employee.RestaurantId);
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
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
        }

        return response;

    }

    public async Task<Response<MenuItemDetailsDto>> GetMenuItem(int menuItemId)
    {
        Response<MenuItemDetailsDto> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menuItem = await _menuItemRepository.GetMenuItem(menuItemId, owner.Id);
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

    public async Task<Response<GetMenuItemEditDto>> GetMenuItemEdit(int menuItemId)
    {
        Response<GetMenuItemEditDto> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menuItem = await _menuItemRepository.GetMenuItemEdit(menuItemId, owner.Id);
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


    public async Task<Response<int>> Update(int menuItemId, EditMenuItemDto editMenuItemDto)
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

            var menuItem = await _menuItemRepository.GetOwnerMenuItem(menuItemId, owner.Id);
            if (menuItem == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            menuItem.Name = editMenuItemDto.Name;
            menuItem.Price = editMenuItemDto.Price;
            menuItem.Description = editMenuItemDto.Description;
            menuItem.IsActive = editMenuItemDto.IsActive;
            menuItem.HasSpecialOffer = editMenuItemDto.HasSpecialOffer;
            menuItem.SpecialOfferPrice = editMenuItemDto.SpecialOfferPrice;

            if (!await _menuItemRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update menu item.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = menuItem.Id;
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
