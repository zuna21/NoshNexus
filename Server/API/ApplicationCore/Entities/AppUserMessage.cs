namespace API;

public class AppUserMessage
{
    public int Id { get; set; }
    public int MessageId { get; set; }
    public int AppUserId { get; set; }
    public bool IsSeen { get; set; }
    public DateTime SeenAt { get; set; }


    // navigation properties
    public Message Message { get; set; }
    public AppUser AppUser { get; set; }
}
