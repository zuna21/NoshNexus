namespace API;

public class AppUserChat
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public int ChatId { get; set; }

    public bool IsSeen { get; set; } = false;
    public DateTime SeenAt { get; set; }

    // navigation property
    public AppUser AppUser { get; set; }
    public Chat Chat { get; set; }
}
