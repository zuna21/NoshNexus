using ApplicationCore.Entities;

namespace API;

public interface INotificationRepository
{
    void AddNotification(Notification notification);
    Task<bool> SaveAllAsync();
}
