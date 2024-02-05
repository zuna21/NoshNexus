using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using Microsoft.AspNetCore.SignalR;

namespace API.Infrastructure;

public class OrderHub(
    IOrderRepository orderRepository,
    IOwnerRepository ownerRepository,
    IAppUserRepository appUserRepository,
    IRestaurantRepository restaurantRepository,
    IEmployeeRepository employeeRepository
) : Hub
{
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IOwnerRepository _ownerRepository = ownerRepository;
    private readonly IAppUserRepository _appUserRepository = appUserRepository;
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;

    public async override Task OnConnectedAsync()
    {
        var username = Context.UserIdentifier;
        var user = await _appUserRepository.GetUserByUsername(username);
        var owner = await _ownerRepository.GetOwnerByUsername(username);
        var employee = await _employeeRepository.GetEmployeeByUsername(username);

        // Za ownera
        if (user != null && owner != null)
        {
            OrderConnection orderConnection = new()
            {
                User = user,
                AppUserId = user.Id,
                ConnectionId = Context.ConnectionId
            };
            var restaurantNames = await _restaurantRepository.GetOwnerRestaurantNames(owner.Id);
            foreach (var restaurantName in restaurantNames)
            {
                await Groups.AddToGroupAsync(orderConnection.ConnectionId, restaurantName);
            }
            _orderRepository.AddOrderConnection(orderConnection);
            await _orderRepository.SaveAllAsync();
        }

        if (user != null && employee != null)
        {
            OrderConnection orderConnection = new()
            {
                AppUserId = user.Id,
                ConnectionId = Context.ConnectionId,
                User = user
            };
            var restaurantName = await _restaurantRepository.GetEmployeeRestaurantName(employee.Id);
            await Groups.AddToGroupAsync(orderConnection.ConnectionId, restaurantName);
            _orderRepository.AddOrderConnection(orderConnection);
            await _orderRepository.SaveAllAsync();
        }


    }

    public async override Task OnDisconnectedAsync(Exception exception)
    {
        var username = Context.UserIdentifier;
        var user = await _appUserRepository.GetUserByUsername(username);
        var owner = await _ownerRepository.GetOwnerByUsername(username);
        var employee = await _employeeRepository.GetEmployeeByUsername(username);

        // Za ownera
        if (owner != null && user != null)
        {
            var orderConnection = await _orderRepository.GetOrderConnectionByUserId(user.Id);
            if (orderConnection == null) return;
            var restaurantNames = await _restaurantRepository.GetOwnerRestaurantNames(owner.Id);
            foreach (var restaurantName in restaurantNames)
            {
                await Groups.RemoveFromGroupAsync(orderConnection.ConnectionId, restaurantName);
            }
            _orderRepository.RemoveConnection(orderConnection);
            await _orderRepository.SaveAllAsync();
        }

        // Za employee
        if(user != null && employee != null)
        {
            var orderConnection = await _orderRepository.GetOrderConnectionByUserId(user.Id);
            if (orderConnection == null) return;
            var restaurantName = await _restaurantRepository.GetEmployeeRestaurantName(employee.Id);
            await Groups.RemoveFromGroupAsync(orderConnection.ConnectionId, restaurantName);
            _orderRepository.RemoveConnection(orderConnection);
            await _orderRepository.SaveAllAsync();
        }
    }

    
}
