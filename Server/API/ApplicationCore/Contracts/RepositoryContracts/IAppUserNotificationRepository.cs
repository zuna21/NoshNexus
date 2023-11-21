namespace API;

public interface IAppUserNotificationRepository
{
    void AddManyAppUserNotifications(List<AppUserNotification> appUserNotifications);
    Task<List<GetNotificationDto>> GetLastNotifications(int userId, int notificationsNumber);
    Task<AppUserNotification> GetUserNotification(int userId, int notificationId);
    Task<int> CountNotSeenNotifications(int userId);
    Task<bool> SaveAllAsync();
}
