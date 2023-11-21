﻿
namespace API;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IAppUserNotificationRepository _appUserNotificationRepository;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUserService _userService;
    public NotificationService(
        INotificationRepository notificationRepository,
        IAppUserNotificationRepository appUserNotificationRepository,
        IAppUserRepository appUserRepository,
        IUserService userService
    )
    {
        _notificationRepository = notificationRepository;
        _appUserNotificationRepository = appUserNotificationRepository;
        _appUserRepository = appUserRepository;
        _userService = userService;
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

    public async Task<Response<GetNotificationForMenuDto>> GetNotificationForMenu(int notificationNumber)
    {
        Response<GetNotificationForMenuDto> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var notifications = await _appUserNotificationRepository.GetLastNotifications(user.Id, notificationNumber);
            if (notifications == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var notSeenNumber = await _appUserNotificationRepository.CountNotSeenNotifications(user.Id);
            var notificationForMenu = new GetNotificationForMenuDto
            {
                NotSeenNumber = notSeenNumber,
                Notifications = notifications
            };

            response.Status = ResponseStatus.Success;
            response.Data = notificationForMenu;
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