

using Microsoft.EntityFrameworkCore;

namespace API;

public class AppUserNotificationRepository : IAppUserNotificationRepository
{
    private readonly DataContext _context;
    public AppUserNotificationRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void AddManyAppUserNotifications(List<AppUserNotification> appUserNotifications)
    {
        _context.AppUserNotifications.AddRange(appUserNotifications);
    }

    public async Task<int> CountNotSeenNotifications(int userId)
    {
        return await _context.AppUserNotifications
            .Where(x => x.IsSeen == false && x.AppUserId == userId)
            .CountAsync();
    }

    public async Task<List<GetNotificationDto>> GetLastNotifications(int userId, int notificationsNumber)
    {
        return await _context.AppUserNotifications
            .Where(x => x.AppUserId == userId)
            .OrderByDescending(x => x.Notification.CretaedAt)
            .Take(notificationsNumber)
            .Select(x => new GetNotificationDto
            {
                Id = x.Notification.Id,
                Title = x.Notification.Title,
                Description = x.Notification.Description,
                IsSeen = x.IsSeen,
                CreatedAt = x.Notification.CretaedAt
            })
            .ToListAsync();
    }

    public async Task<AppUserNotification> GetUserNotification(int userId, int notificationId)
    {
        return await _context.AppUserNotifications.FirstOrDefaultAsync(x => x.AppUserId == userId && x.NotificationId == notificationId);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
