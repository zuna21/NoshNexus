
namespace API;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IAppUserNotificationRepository _appUserNotificationRepository;
    private readonly IAppUserRepository _appUserRepository;
    public NotificationService(
        INotificationRepository notificationRepository,
        IAppUserNotificationRepository appUserNotificationRepository,
        IAppUserRepository appUserRepository
    )
    {
        _notificationRepository = notificationRepository;
        _appUserNotificationRepository = appUserNotificationRepository;
        _appUserRepository = appUserRepository;
    }
    public async Task<Response<bool>> CreateNotificationForAllUsers(CreateNotificationDto createNotificationDto)
    {
        Response<bool> response = new();
        try
        {
            var notification = new Notification
            {
                Title = createNotificationDto.Title,
                Description = createNotificationDto.Description
            };
            _notificationRepository.AddNotification(notification);
            if (!await _notificationRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create notification.";
                return response;
            }

            var users = await _appUserRepository.GetAllUsers();
            if (users == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var userNotifications = users.Select(x => new AppUserNotification
            {
                AppUser = x,
                AppUserId = x.Id,
                Notification = notification,
                NotificationId = notification.Id
            }).ToList();

            _appUserNotificationRepository.AddManyAppUserNotifications(userNotifications);
            if (!await _appUserNotificationRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create notification for all users.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = true;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }
}
