using ApplicationCore.DTOs;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface INotificationService
{
    Task<Response<bool>> CreateNotificationForAllUsers(CreateNotificationDto createNotificationDto);
    Task<Response<GetNotificationForMenuDto>> GetNotificationForMenu(int notificationNumber);
    Task<Response<int>> MarkNotificationAsRead(int notificationId);
    Task<Response<List<GetNotificationDto>>> GetAllNotifications();
    Task<Response<bool>> MarkAllNotificationsAsRead();
}
