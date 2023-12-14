namespace ApplicationCore;

public interface IHubConnectionRepository
{
    void AddConnection(HubConnection hubConnection);
    void RemoveConnection(HubConnection hubConnection);
    Task<ICollection<string>> GetUserConnectionIdsByType(int userId, HubConnectionType type);
    HubConnection GetHubConnectionByConnectionId(string connectionId);
    bool SaveAllSync();
}
