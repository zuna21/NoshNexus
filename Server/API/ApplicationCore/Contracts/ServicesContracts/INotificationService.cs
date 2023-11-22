namespace API;

public interface INotificationService
{
    Task<Response<bool>> CreateNotificationForAllUsers(CreateNotificationDto createNotificationDto);
    Task<Response<GetNotificationForMenuDto>> GetNotificationForMenu(int notificationNumber);
    Task<Response<int>> MarkNotificationAsRead(int notificationId);
    Task<Response<bool>> MarkAllNotificationsAsRead();
}
