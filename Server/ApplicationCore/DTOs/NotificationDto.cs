namespace ApplicationCore.DTOs;

public class NotificationDto
{

}

public class CreateNotificationDto 
{
    public string Title { get; set; }
    public string Description { get; set; }
}

public class GetNotificationDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsSeen { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class GetNotificationForMenuDto
{
    public int NotSeenNumber { get; set; }
    public List<GetNotificationDto> Notifications { get; set; }
}
