using ApplicationCore.Entities;

namespace ApplicationCore;

public class ChatConnection
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public string ConnectionId { get; set; }
}
