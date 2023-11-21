

using Microsoft.EntityFrameworkCore;

namespace API;

public class NotificationRepository : INotificationRepository
{
    private readonly DataContext _context;
    public NotificationRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void AddNotification(Notification notification)
    {
        _context.Notifications.Add(notification);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
