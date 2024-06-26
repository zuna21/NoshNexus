﻿

using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using EmployeeQueryParams = ApplicationCore.QueryParams.EmployeeQueryParams;

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
    public async Task<Response<int>> Create(OwnerDtos.CreateMenuDto createMenuDto)
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

    public async Task<Response<OwnerDtos.GetMenuDetailsDto>> GetMenu(int menuId, OwnerQueryParams.MenuItemsQueryParams menuItemsQueryParams)
    {
        Response<OwnerDtos.GetMenuDetailsDto> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = await _menuRepository.GetMenu(menuId, owner.Id, menuItemsQueryParams);
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

    public async Task<Response<OwnerDtos.GetMenuEditDto>> GetMenuEdit(int menuId)
    {
        Response<OwnerDtos.GetMenuEditDto> response = new();
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


    public async Task<Response<PagedList<OwnerDtos.MenuCardDto>>> GetMenus(OwnerQueryParams.MenusQueryParams menusQueryParams)
    {
        Response<PagedList<OwnerDtos.MenuCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menus = await _menuRepository.GetMenus(owner.Id, menusQueryParams);

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

    public async Task<Response<int>> Update(int menuId, OwnerDtos.EditMenuDto editMenuDto)
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

    public async Task<Response<PagedList<OwnerDtos.MenuCardDto>>> GetEmployeeMenuCardDtos(EmployeeQueryParams.MenusQueryParams menusQueryParams)
    {
        Response<PagedList<OwnerDtos.MenuCardDto>> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menus = await _menuRepository.GetEmployeeMenuCardDtos(employee.RestaurantId, menusQueryParams);
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

    public async Task<Response<OwnerDtos.GetMenuDetailsDto>> GetEmployeeMenuDetails(int id, OwnerQueryParams.MenuItemsQueryParams menuItemsQueryParams)
    {
        Response<OwnerDtos.GetMenuDetailsDto> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menu = await _menuRepository.GetEmployeeMenu(id, employee.RestaurantId, menuItemsQueryParams);
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

    public async Task<Response<EmployeeDtos.GetMenuEditDto>> GetEmployeeMenuEdit(int menuId)
    {
        Response<EmployeeDtos.GetMenuEditDto> response = new();
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

    public async Task<Response<int>> EmployeeUpdate(int menuId, EmployeeDtos.EditMenuDto employeeEditMenuDto)
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

            if (!employee.CanEditMenus)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "You have no permission to update menu.";
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


    public async Task<Response<ICollection<OwnerDtos.GetRestaurantMenusForSelectDto>>> GetRestaurantMenusForSelect(int restaurantId)
    {
        Response<ICollection<OwnerDtos.GetRestaurantMenusForSelectDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _menuRepository.GetRestaurantMenusForSelect(restaurantId, owner.Id);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<CustomerDtos.MenuCardDto>>> GetCustomerRestaurantMenus(int restaurantId, CustomerQueryParams.MenusQueryParams menusQueryParams)
    {
        Response<ICollection<CustomerDtos.MenuCardDto>> response = new();
        try
        {
            response.Status = ResponseStatus.Success;
            response.Data = await _menuRepository.GetCustomerRestaurantMenus(restaurantId, menusQueryParams);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }
        
        return response;
    }

    public async Task<Response<CustomerDtos.MenuDto>> GetCustomerMenu(int menuId)
    {
        Response<CustomerDtos.MenuDto> response = new();
        try
        {
            var menu = await _menuRepository.GetCustomerMenu(menuId);
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

    public async Task<Response<int>> EmployeeCreate(EmployeeDtos.CreateMenuDto createMenuDto)
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

            if (!employee.CanEditMenus)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "You have no permission to create menu.";
                return response;
            }

            var restaurant = await _restaurantRepository.GetAnyRestaurantById(employee.RestaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            Menu menu = new()
            {
                Description = createMenuDto.Description,
                IsActive = createMenuDto.IsActive,
                Name = createMenuDto.Name,
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
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }
}
