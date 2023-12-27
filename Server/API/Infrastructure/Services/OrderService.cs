using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.SignalR;

namespace API;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuItemRepository _menuItemRepository;
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IUserService _userService;
    private readonly IHubContext<OrderHub> _orderHub;
    private readonly IHubConnectionRepository _hubConnectionRepository;
    private readonly IAppUserRepository _appUserRepository;
    private readonly ITableRepository _tableRepository;
    public OrderService(
        IOrderRepository orderRepository,
        IUserService userService,
        IHubContext<OrderHub> orderHub,
        IHubConnectionRepository hubConnectionRepository,
        IAppUserRepository appUserRepository,
        IRestaurantRepository restaurantRepository,
        IMenuItemRepository menuItemRepository,
        ITableRepository tableRepository
    )
    {
        _orderRepository = orderRepository;
        _userService = userService;
        _orderHub = orderHub;
        _hubConnectionRepository = hubConnectionRepository;
        _appUserRepository = appUserRepository;
        _restaurantRepository = restaurantRepository;
        _menuItemRepository = menuItemRepository;
        _tableRepository = tableRepository;
    }

    public async Task<Response<int>> AcceptOrder(int orderId)
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
            if (restaurant == null) {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var order = await _orderRepository.GetRestaurantOrderById(orderId, employee.RestaurantId);
            if (order == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            var user = await _appUserRepository.GetAppUserByCustomerId(order.CustomerId);
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            order.Status = OrderStatus.Accepted;
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to accept order.";
            }

            var connections = await _hubConnectionRepository.GetUserConnectionIdsByType(user.Id, HubConnectionType.Order);
            await _orderHub.Clients.GroupExcept(restaurant.Name, connections).SendAsync("RemoveOrder", order.Id);
            await _orderHub.Clients.Clients(connections).SendAsync("AcceptOrder", order.Id);

            response.Status = ResponseStatus.Success;
            response.Data = order.Id;


        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
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

            var restaurant = await _restaurantRepository.GetAnyRestaurantById(restaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var table = await _tableRepository.GetRestaurantTable(createOrderDto.TableId, restaurant.Id);
            if (table == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            List<MenuItem> menuItems = [];
            foreach (var menuItemId in createOrderDto.MenuItemIds)
            {
                var menuItem = await _menuItemRepository.GetRestaurantMenuItemEntity(restaurant.Id, menuItemId);
                if (menuItem == null) continue;
                menuItem.OrderCount++;
                menuItems.Add(menuItem);
            }

            if (menuItems.Count > 0)
            {
                if (!await _menuItemRepository.SaveAllAsync())
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to count order";
                    return response;
                }
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

            OrderCardDto orderCardDto = new()
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                DeclineReason = order.DeclineReason,
                Note = order.Note,
                TableName = table.Name,
                Status = order.Status.ToString(),
                TotalItems = order.TotalItems,
                TotalPrice = order.TotalPrice,
                Restaurant = new OrderRestaurantDto
                {
                    Id = restaurant.Id,
                    Name = restaurant.Name
                },
                User = new OrderCardUserDto
                {
                    Id = customer.Id,
                    FirstName = "",
                    LastName = "",
                    ProfileImage = "",
                    Username = customer.UniqueUsername
                },
                Items = menuItems
                    .Select(x => new OrderMenuItemDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.HasSpecialOffer ? x.SpecialOfferPrice : x.Price
                    })
                    .ToList()
            };

            await _orderHub.Clients.Group(restaurant.Name).SendAsync("ReceiveOrder", orderCardDto);

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

    public async Task<Response<int>> DeclineOrder(int orderId, DeclineReasonDto declineReasonDto)
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

            var order = await _orderRepository.GetRestaurantOrderById(orderId, employee.RestaurantId);
            if (order == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            order.Status = OrderStatus.Declined;
            order.DeclineReason = declineReasonDto.Reason;
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to decline order.";
                return response;
            }

            var user = await _appUserRepository.GetAppUserByCustomerId(order.CustomerId);
            if (user == null) 
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var connections = await _hubConnectionRepository.GetUserConnectionIdsByType(user.Id, HubConnectionType.Order);
            await _orderHub.Clients.GroupExcept(restaurant.Name, connections).SendAsync("RemoveOrder", order.Id);
            await _orderHub.Clients.Clients(connections).SendAsync("DeclineOrder", new{Id = order.Id, Reason = order.DeclineReason});

            response.Status = ResponseStatus.Success;
            response.Data = order.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<OrderCardDto>>> GetCustomerAcceptedOrders(string sq)
    {
        Response<ICollection<OrderCardDto>> response = new();
        try
        {
            var customer = await _userService.GetCustomer();
            if (customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _orderRepository.GetCustomerAcceptedOrders(customer.Id, sq);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<OrderCardDto>>> GetCustomerDeclinedOrders(string sq)
    {
        Response<ICollection<OrderCardDto>> response = new();
        try
        {
            var customer = await _userService.GetCustomer();
            if (customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _orderRepository.GetCustomerDeclinedOrders(customer.Id, sq);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<CustomerLiveRestaurantOrdersDto>> GetCustomerInProgressOrders(int restaurantId)
    {
        Response<CustomerLiveRestaurantOrdersDto> response = new();
        try
        {
            var liveOrders = await _orderRepository.GetCustomerInProgressOrders(restaurantId);
            if (liveOrders == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = liveOrders;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<OrderCardDto>>> GetCustomerOrders(string sq)
    {
        Response<ICollection<OrderCardDto>> response = new();
        try
        {
            var customer = await _userService.GetCustomer();
            if (customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _orderRepository.GetCustomerOrders(customer.Id, sq);
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
