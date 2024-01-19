using ApplicationCore.Entities;

namespace ApplicationCore;

public class OrderConnection
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public string ConnectionId { get; set; }

    public AppUser User { get; set; }
}
