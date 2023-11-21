namespace API;

public interface IAppUserNotificationRepository
{
    void AddManyAppUserNotifications(List<AppUserNotification> appUserNotifications);
    Task<bool> SaveAllAsync();
}
