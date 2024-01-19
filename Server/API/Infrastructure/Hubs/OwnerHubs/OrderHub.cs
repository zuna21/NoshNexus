using ApplicationCore.Contracts.RepositoryContracts;
using Microsoft.AspNetCore.SignalR;

namespace API.Infrastructure.Hubs.OwnerHubs;

public class OrderHub(
    IAppUserRepository appUserRepository
) : Hub
{
    private readonly IAppUserRepository _appUserRepository = appUserRepository;

    public async override Task OnConnectedAsync()
    {
        var username = Context.UserIdentifier;
        var user = await _appUserRepository.GetUserByUsername(username);
        if (user == null) return;
        
    }
}
