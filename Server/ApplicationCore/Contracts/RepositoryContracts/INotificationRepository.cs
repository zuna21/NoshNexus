using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface INotificationRepository
{
    void AddNotification(Notification notification);
    Task<bool> SaveAllAsync();
}
