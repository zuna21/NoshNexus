using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace API;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IUserService _userService;
    public TableService(
        ITableRepository tableRepository,
        IUserService userService,
        IRestaurantRepository restaurantRepository
    )
    {
        _tableRepository = tableRepository;
        _userService = userService;
        _restaurantRepository = restaurantRepository;
    }
    public async Task<Response<bool>> CreateTables(ICollection<OwnerDtos.TableCardDto> tableCardDtos)
    {
        Response<bool> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            List<Restaurant> restaurants = new();
            foreach (var tableDto in tableCardDtos)
            {
                if (!restaurants.Select(x => x.Id).Contains(tableDto.Restaurant.Id))
                {
                    var restaurant = await _restaurantRepository.GetOwnerRestaurant(tableDto.Restaurant.Id, owner.Id);
                    if (restaurant == null)
                    {
                        response.Status = ResponseStatus.NotFound;
                        return response;
                    }
                    restaurants.Add(restaurant);
                }
                var table = new Table
                {
                    Name = tableDto.Name,
                    RestaurantId = tableDto.Restaurant.Id,
                    Restaurant = restaurants.FirstOrDefault(x => x.Id == tableDto.Restaurant.Id)
                };
                _tableRepository.AddTable(table);
            }

            if (!await _tableRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create tables";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = true;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }
        return response;
    }

    public async Task<Response<bool>> Delete(int tableId)
    {
        Response<bool> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var table = await _tableRepository.GetOwnerTable(tableId, owner.Id);
            if (table == null) 
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            _tableRepository.Delete(table);
            if (!await _tableRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete table.";
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

    public async Task<Response<bool>> EmployeeCreate(ICollection<OwnerDtos.TableCardDto> tableCardDtos)
    {
        Response<bool> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (tableCardDtos.Count <= 0)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please add at least one table.";
                return response;
            }

            var restaurant = await _restaurantRepository.GetAnyRestaurantById(employee.RestaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            List<Table> tables = [];
            foreach (var tableCardDto in tableCardDtos)
            {
                Table table = new Table
                {
                    Name = tableCardDto.Name,
                    RestaurantId = restaurant.Id,
                    Restaurant = restaurant
                };

                tables.Add(table);
            }

            _tableRepository.AddMany(tables);
            if (!await _tableRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Something went wrong.";
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

    public async Task<Response<bool>> EmployeeDelete(int tableId)
    {
        Response<bool> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var table = await _tableRepository.GetRestaurantTable(tableId, employee.RestaurantId);
            if (table == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            _tableRepository.Delete(table);
            if (!await _tableRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete table.";
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

    public async Task<Response<ICollection<OwnerDtos.TableCardDto>>> GetEmployeeTables()
    {
        Response<ICollection<OwnerDtos.TableCardDto>> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var tables = await _tableRepository.GetEmployeeTables(employee.RestaurantId);
            if (tables == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = tables;
            
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<CustomerDtos.TableDto>>> GetRestaurantTables(int restaurantId)
    {
        Response<ICollection<CustomerDtos.TableDto>> response = new();
        try
        {
            response.Status = ResponseStatus.Success;
            response.Data = await _tableRepository.GetRestaurantTables(restaurantId);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<PagedList<OwnerDtos.TableCardDto>>> GetTables(OwnerQueryParams.TablesQueryParams tablesQueryParams)
    {
        Response<PagedList<OwnerDtos.TableCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var tables = await _tableRepository.GetTables(owner.Id, tablesQueryParams);
            if (tables == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = tables;
        }
        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }
}
