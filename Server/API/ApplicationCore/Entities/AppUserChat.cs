namespace API;

public class AppUserChat
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public int ChatId { get; set; }



    // navigation property
    public AppUser AppUser { get; set; }
    public Chat Chat { get; set; }
}
