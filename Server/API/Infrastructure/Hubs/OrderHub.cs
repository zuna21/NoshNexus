using ApplicationCore.Contracts.RepositoryContracts;
using Microsoft.AspNetCore.SignalR;

namespace API;

public class OrderHub(
    IRestaurantRepository restaurantRepository,
    IEmployeeRepository employeeRepository
) : Hub
{
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
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

}
