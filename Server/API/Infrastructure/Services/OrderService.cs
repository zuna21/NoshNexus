using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.SignalR;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace API;

public class OrderService(
    IOrderRepository orderRepository,
    IUserService userService,
    IRestaurantRepository restaurantRepository,
    IMenuItemRepository menuItemRepository,
    ITableRepository tableRepository,
    ICustomerRepository customerRepository
    ) : IOrderService
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IMenuItemRepository _menuItemRepository = menuItemRepository;
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IUserService _userService = userService;
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Response<int>> AcceptOrder(int orderId)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            var employee = await _userService.GetEmployee();
            if (owner == null && employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            order.Status = OrderStatus.Accepted;
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to accept order.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = order.Id;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
        }

        return response;
    }

    public async Task<Response<int>> BlockCustomer(int orderId)
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

            var order = await _orderRepository.GetOrderById(orderId);
            var restaurant = await _restaurantRepository.GetOwnerRestaurant(order.RestaurantId, owner.Id);
            var customer = await _customerRepository.GetCustomerById(order.CustomerId);

            if (order == null || restaurant == null || customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            RestaurantBlockedCustomers restaurantBlockedCustomers = new()
            {
                RestaurantId = restaurant.Id,
                CustomerId = customer.Id,
                Restaurant = restaurant,
                Customer = customer
            };

            _orderRepository.BlockCustomer(restaurantBlockedCustomers);
            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to block user";
                return response;
            }

            order.Status = OrderStatus.Declined;
            order.DeclineReason = $"User {customer.UniqueUsername} is blocked.";

            if (!await _orderRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to decline order.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = order.Id;


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<bool>> CreateOrder(int restaurantId, CustomerDtos.CreateOrderDto createOrderDto)
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
                if (menuItem == null)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Please select all menu items from one restaurant.";
                    return response;
                }
                menuItem.OrderCount++;
                menuItems.Add(menuItem);
            }

            if (menuItems.Count <= 0)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Something went wrong.";
                return response;
            }


            if (!await _menuItemRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to increase order count.";
                return response;
            }



            int totalItems = menuItems.Count;
            double totalPrice = 0;
            foreach (var menuItem in menuItems)
            {
                if (menuItem.HasSpecialOffer) totalPrice += menuItem.SpecialOfferPrice;
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
        catch (Exception ex)
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
            var owner = await _userService.GetOwner();
            var employee = await _userService.GetEmployee();
            if (owner == null && employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var order = await _orderRepository.GetOrderById(orderId);
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

            response.Status = ResponseStatus.Success;
            response.Data = order.Id;

        }
        catch (Exception ex)
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
        catch (Exception ex)
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
        catch (Exception ex)
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
        catch (Exception ex)
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
        catch (Exception ex)
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<PagedList<OrderCardDto>>> GetOrdersHistory(OwnerQueryParams.OrdersHistoryQueryParams ordersHistoryQueryParams)
    {
        Response<PagedList<OrderCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _orderRepository.GetOrdersHistory(owner.Id, ordersHistoryQueryParams);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<OrderCardDto>>> GetOwnerInProgressOrders(OwnerQueryParams.OrdersQueryParams ordersQueryParams)
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

            var orders = await _orderRepository.GetOwnerInProgressOrders(owner.Id, ordersQueryParams);
            if (orders == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = orders;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
        }

        return response;
    }
}
