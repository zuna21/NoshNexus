namespace ApplicationCore.Entities;

public class Message
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public int ChatId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Navigation properties 
    public AppUser Sender { get; set; }
    public Chat Chat { get; set; }
}
