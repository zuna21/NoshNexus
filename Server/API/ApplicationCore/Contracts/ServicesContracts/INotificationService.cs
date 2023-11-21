namespace API;

public interface INotificationService
{
    Task<Response<bool>> CreateNotificationForAllUsers(CreateNotificationDto createNotificationDto);
}
