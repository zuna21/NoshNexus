using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using Microsoft.AspNetCore.SignalR;

namespace API;

public class OrderHub(
    IRestaurantRepository restaurantRepository,
    IEmployeeRepository employeeRepository,
    IHubConnectionRepository hubConnectionRepository,
    IAppUserRepository appUserRepository
) : Hub
{
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IHubConnectionRepository _hubConnectionRepository = hubConnectionRepository;
    private readonly IAppUserRepository _appUserRepository = appUserRepository;


    public async Task JoinGroup(int restaurantId)
    {
        var username = Context.UserIdentifier;
        var user = _appUserRepository.GetUserByUsernameSync(username);
        if (user == null) return;
        var restaurant = _restaurantRepository.GetRestaurantByIdSync(restaurantId);
        if (restaurant == null) return;
        HubConnection hubConnection = new()
        {
            AppUserId = user.Id,
            AppUser = user,
            ConnectionId = Context.ConnectionId,
            Type = HubConnectionType.Order,
            GroupName = restaurant.Name
        };
        _hubConnectionRepository.AddConnection(hubConnection);
        if (!_hubConnectionRepository.SaveAllSync()) return;
        await Groups.AddToGroupAsync(Context.ConnectionId, restaurant.Name);
    }

    public async Task JoinGroupEmployee()
    {
        var username = Context.UserIdentifier;
        var employee = _employeeRepository.GetEmployeeByUsernameSync(username);
        if (employee == null) return;
        var restaurant = _restaurantRepository.GetRestaurantByIdSync(employee.RestaurantId);
        if (restaurant == null) return;
        var user = _appUserRepository.GetUserByUsernameSync(employee.UniqueUsername);
        if (user == null) return;
        HubConnection hubConnection = new()
        {
            AppUserId = user.Id,
            AppUser = user,
            ConnectionId = Context.ConnectionId,
            GroupName = restaurant.Name
        };
        _hubConnectionRepository.AddConnection(hubConnection);
        if (!_hubConnectionRepository.SaveAllSync()) return;
        await Groups.AddToGroupAsync(Context.ConnectionId, restaurant.Name);
    }


    public async override Task OnDisconnectedAsync(Exception exception)
    {
        var hubConnection = _hubConnectionRepository.GetHubConnectionByConnectionId(Context.ConnectionId);
        if (hubConnection == null) return;
        _hubConnectionRepository.RemoveConnection(hubConnection);
        if(!_hubConnectionRepository.SaveAllSync()) return;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, hubConnection.GroupName);
    }

}
