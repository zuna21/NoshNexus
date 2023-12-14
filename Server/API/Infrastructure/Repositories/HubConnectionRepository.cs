using ApplicationCore;
using Microsoft.EntityFrameworkCore;

namespace API;

public class HubConnectionRepository(
    DataContext dataContext
) : IHubConnectionRepository
{
    private readonly DataContext _context = dataContext;


    public void AddConnection(HubConnection hubConnection)
    {
        _context.HubConnections.Add(hubConnection);
    }

    public HubConnection GetHubConnectionByConnectionId(string connectionId)
    {
        return _context.HubConnections
            .Where(x => string.Equals(x.ConnectionId, connectionId))
            .FirstOrDefault();
    }

    public async Task<ICollection<string>> GetUserConnectionIdsByType(int userId, HubConnectionType type)
    {
        return await _context.HubConnections
            .Where(x => x.AppUserId == userId && x.Type == type)
            .Select(x => x.ConnectionId)
            .ToListAsync();
    }

    public void RemoveConnection(HubConnection hubConnection)
    {
        _context.HubConnections.Remove(hubConnection);
    }

    public bool SaveAllSync()
    {
        return _context.SaveChanges() > 0;
    }
}
