namespace API;

public interface INotificationService
{
    Task<Response<bool>> CreateNotificationForAllUsers(CreateNotificationDto createNotificationDto);
    Task<Response<GetNotificationForMenuDto>> GetNotificationForMenu(int notificationNumber);
}
