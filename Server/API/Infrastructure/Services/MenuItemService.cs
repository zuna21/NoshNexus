using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace API;

public class MenuItemService : IMenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IUserService _userService;
    private readonly IMenuRepository _menuRepository;
    public MenuItemService(
        IMenuItemRepository menuItemRepository,
        IUserService userService,
        IMenuRepository menuRepository
    )
    {
        _menuItemRepository = menuItemRepository;
        _userService = userService;
        _menuRepository = menuRepository;
    }
    public async Task<Response<OwnerDtos.MenuItemCardDto>> Create(int menuId, OwnerDtos.CreateMenuItemDto createMenuItemDto)
    {
        Response<OwnerDtos.MenuItemCardDto> response = new();
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
            response.Data = new OwnerDtos.MenuItemCardDto
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

    public async Task<Response<OwnerDtos.MenuItemCardDto>> EmployeeCreate(int menuId, OwnerDtos.CreateMenuItemDto createMenuItemDto)
    {
        Response<OwnerDtos.MenuItemCardDto> response = new();
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
            response.Data = new OwnerDtos.MenuItemCardDto
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

    public async Task<Response<int>> EmployeeUpdate(int menuItemId, OwnerDtos.EditMenuItemDto editMenuItemDto)
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


    public async Task<Response<OwnerDtos.GetMenuItemDetailsDto>> GetEmployeeMenuItem(int menuItemId)
    {
        Response<OwnerDtos.GetMenuItemDetailsDto> response = new();
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

    public async Task<Response<OwnerDtos.GetMenuItemEditDto>> GetEmployeeMenuItemEdit(int menuItemId)
    {
        Response<OwnerDtos.GetMenuItemEditDto> response = new();
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

    public async Task<Response<OwnerDtos.GetMenuItemDetailsDto>> GetMenuItem(int menuItemId)
    {
        Response<OwnerDtos.GetMenuItemDetailsDto> response = new();
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

    public async Task<Response<OwnerDtos.GetMenuItemEditDto>> GetMenuItemEdit(int menuItemId)
    {
        Response<OwnerDtos.GetMenuItemEditDto> response = new();
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

    public async Task<Response<ICollection<CustomerDtos.MenuItemCardDto>>> GetCustomerRestaurantMenuItems(int restaurantId, CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams)
    {
        Response<ICollection<CustomerDtos.MenuItemCardDto>> response = new();
        try
        {
            response.Status = ResponseStatus.Success;
            response.Data = await _menuItemRepository.GetCustomerRestaurantMenuItems(restaurantId, menuItemsQueryParams);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<int>> Update(int menuItemId, OwnerDtos.EditMenuItemDto editMenuItemDto)
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

    public async Task<Response<ICollection<CustomerDtos.MenuItemCardDto>>> GetCustomerMenuMenuItems(int menuId, CustomerQueryParams.MenuItemsQueryParams menuItemsQueryParams)
    {
        Response<ICollection<CustomerDtos.MenuItemCardDto>> response = new();
        try
        {
            response.Status = ResponseStatus.Success;
            response.Data = await _menuItemRepository.GetCustomerMenuMenuItems(menuId, menuItemsQueryParams);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<bool>> AddFavouriteMenuItem(int menuItemId)
    {
        Response<bool> response = new();
        try
        {
            var customer = await _userService.GetCustomer();
            if (customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menuItem = await _menuItemRepository.GetAnyMenuItemById(menuItemId);
            if (menuItem == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            FavouriteCustomerMenuItem favouriteCustomerMenuItem = new()
            {
                CustomerId = customer.Id,
                Customer = customer,
                MenuItemId = menuItem.Id,
                MenuItem = menuItem
            };

            _menuItemRepository.AddFavouriteMenuItem(favouriteCustomerMenuItem);
            if (!await _menuItemRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to add favourite menu item.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = true;
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
