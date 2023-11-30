namespace ApplicationCore.Entities;

public class AppUserNotification
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public int NotificationId { get; set; }
    public bool IsSeen { get; set; } = false;
    public DateTime SeenAt { get; set; }


    // navigation properties
    public AppUser AppUser { get; set; }
    public Notification Notification { get; set; }
}
