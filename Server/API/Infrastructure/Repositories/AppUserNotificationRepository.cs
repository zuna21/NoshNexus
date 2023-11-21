
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

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
