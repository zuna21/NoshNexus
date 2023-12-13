using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.SignalR;

namespace API;

public class OrderHub(
    IOrderRepository orderRepository,
    IRestaurantRepository restaurantRepository,
    IMenuItemRepository menuItemRepository,
    ICustomerRepository customerRepository,
    ITableRepository tableRepository,
    IEmployeeRepository employeeRepository
) : Hub
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IMenuItemRepository _menuItemRepository = menuItemRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ITableRepository _tableRepository = tableRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;


    public async Task JoinGroup(int restaurantId)
    {
        var restaurant = _restaurantRepository.GetRestaurantByIdSync(restaurantId);
        if (restaurant == null) return;
        await Groups.AddToGroupAsync(Context.ConnectionId, restaurant.Name);
    }

    public async Task JoinGroupEmployee()
    {
        var username = Context.UserIdentifier;
        var employee = _employeeRepository.GetEmployeeByUsernameSync(username);
        if (employee == null) return;
        var restaurant = _restaurantRepository.GetRestaurantByIdSync(employee.RestaurantId);
        if (restaurant == null) return;
        await Groups.AddToGroupAsync(Context.ConnectionId, restaurant.Name);
    }

    public async Task LeaveGroup(int restaurantId)
    {
        var restaurant = _restaurantRepository.GetRestaurantByIdSync(restaurantId);
        if (restaurant == null) return;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, restaurant.Name);
    }

    public async Task LeaveGroupEmployee()
    {
        var username = Context.UserIdentifier;
        var employee = _employeeRepository.GetEmployeeByUsernameSync(username);
        if (employee == null) return;
        var restaurant = _restaurantRepository.GetRestaurantByIdSync(employee.RestaurantId);
        if (restaurant == null) return;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, restaurant.Name);
    }

    public async Task CreateOrder(int restaurantId, CreateOrderDto createOrderDto)
    {
        var username = Context.UserIdentifier;
        var user = _customerRepository.GetCustomerByUsernameSync(username);
        if (user == null) return;
        var restaurant = _restaurantRepository.GetRestaurantByIdSync(restaurantId);
        if (restaurant == null) return;
        var table = _tableRepository.GetTableByIdSync(createOrderDto.TableId);
        if (table == null) return;
        var menuItems = _menuItemRepository.GetRestaurantMenuItemsSync(createOrderDto.MenuItemIds, restaurant.Id);
        if (menuItems.Count <= 0) return;

        double totalPrice = 0;
        foreach (var menuItem in menuItems)
        {
            totalPrice += menuItem.HasSpecialOffer ? menuItem.SpecialOfferPrice : menuItem.Price;
        }

        Order order = new()
        {
            CustomerId = user.Id,
            Customer = user,
            Note = createOrderDto.Note,
            RestaurantId = restaurant.Id,
            Restaurant = restaurant,
            Status = OrderStatus.InProgress,
            TableId = table.Id,
            Table = table,
            TotalItems = menuItems.Count,
            TotalPrice = totalPrice
        };
        _orderRepository.Create(order);
        if (!_orderRepository.SaveAllSync()) return;
        var orderMenuItems = menuItems.Select(x => new OrderMenuItem
        {
            MenuItemId = x.Id,
            MenuItem = x,
            OrderId = order.Id,
            Order = order
        })
        .ToList();

        _orderRepository.CreateOrderMenuItems(orderMenuItems);
        if (!_orderRepository.SaveAllSync()) return;

        OrderCardDto orderCardDto = new()
        {
            Id = order.Id,
            DeclineReason = order.DeclineReason,
            CreatedAt = order.CreatedAt,
            Note = order.Note,
            Restaurant = new OrderRestaurantDto
            {
                Id = restaurant.Id,
                Name = restaurant.Name
            },
            Status = "inProgress",
            TableName = table.Name,
            User = new OrderCardUserDto
            {
                Id = user.Id,
                Username = user.UniqueUsername,
                FirstName = "",
                LastName = "",
                ProfileImage = ""
            },
            TotalItems = order.TotalItems,
            TotalPrice = order.TotalPrice,
            Items = menuItems.Select(mi => new OrderMenuItemDto
            {
                Id = mi.Id,
                Name = mi.Name,
                Price = mi.HasSpecialOffer ? mi.SpecialOfferPrice : mi.Price
            })
            .ToList()
        };


        await Clients.Group(restaurant.Name).SendAsync("ReceiveOrder", orderCardDto);
    }

}
