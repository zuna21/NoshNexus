namespace ApplicationCore.Entities;

public class Chat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UniqueName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Navigation properies
    public List<AppUserChat> AppUserChats { get; set; } = new();
    public List<Message> Messages { get; set; } = new();
}

