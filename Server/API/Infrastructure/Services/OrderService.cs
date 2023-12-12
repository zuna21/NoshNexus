

using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICustomerService _customerService;
    private readonly ITableService _tableService;
    private readonly IMenuItemService _menuItemService;
    private readonly IRestaurantService _restaurantService;
    private readonly IOwnerService _ownerService;
    private readonly IUserService _userService;
    public OrderService(
        IOrderRepository orderRepository,
        ICustomerService customerService,
        ITableService tableService,
        IMenuItemService menuItemService,
        IRestaurantService restaurantService,
        IOwnerService ownerService,
        IUserService userService
    )
    {
        _orderRepository = orderRepository;
        _customerService = customerService;
        _tableService = tableService;
        _menuItemService = menuItemService;
        _restaurantService = restaurantService;
        _ownerService = ownerService;
        _userService = userService;
    }
    public async Task<Response<bool>> CreateOrder(int restaurantId, CreateOrderDto createOrderDto)
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

            var restaurant = await _restaurantService.GetAnyRestaurantById(restaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var table = await _tableService.GetRestaurantTable(createOrderDto.TableId, restaurant.Id);
            if (table == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var menuItems = await _menuItemService.GetRestaurantMenuItems(createOrderDto.MenuItemIds, restaurant.Id);
            if (menuItems == null || menuItems.Count <= 0)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "You have to select at least one menu item.";
                return response;
            }

            int totalItems = menuItems.Count;
            double totalPrice = 0;
            foreach (var menuItem in menuItems)
            {
                if(menuItem.HasSpecialOffer) totalPrice += menuItem.SpecialOfferPrice;
                else totalPrice += menuItem.Price;
            }

            var order = new Order
            {
                CustomerId = customer.Id,
                Customer = customer,
                RestaurantId = restaurant.Id,
                Restaurant = restaurant,
                TableId = table.Id,
                Table = table,
                Note = createOrderDto.Note,
                TotalItems = totalItems,
                TotalPrice = totalPrice
            };

            _orderRepository.Create(order);
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create order";
                return response;
            }

            var orderMenuItems = menuItems.Select(x => new OrderMenuItem
            {
                MenuItemId = x.Id,
                MenuItem = x,
                OrderId = order.Id,
                Order = order
            })
            .ToList();

            _orderRepository.CreateOrderMenuItems(orderMenuItems);
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create order.";
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

    public async Task<Response<ICollection<OrderCardDto>>> GetEmployeeInProgressOrders()
    {
        Response<ICollection<OrderCardDto>> response = new();
        try
        {
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _orderRepository.GetEmployeeInProgressOrders(employee.RestaurantId);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<OrderCardDto>>> GetOwnerInProgressOrders()
    {
        Response<ICollection<OrderCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var orders = await _orderRepository.GetOwnerInProgressOrders(owner.Id);
            if (orders == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = orders;

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
