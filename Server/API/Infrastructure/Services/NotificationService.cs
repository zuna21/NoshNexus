﻿

using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.SignalR;

namespace API;

public class NotificationService(
    INotificationRepository notificationRepository,
    IAppUserNotificationRepository appUserNotificationRepository,
    IAppUserRepository appUserRepository,
    IUserService userService,
    IHubContext<NotificationHub> notificationHub
    ) : INotificationService
{
    private readonly INotificationRepository _notificationRepository = notificationRepository;
    private readonly IAppUserNotificationRepository _appUserNotificationRepository = appUserNotificationRepository;
    private readonly IAppUserRepository _appUserRepository = appUserRepository;
    private readonly IUserService _userService = userService;
    private readonly IHubContext<NotificationHub> _notificationHub = notificationHub;

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

            var hubNotification = new GetNotificationDto
            {
                Id = notification.Id,
                CreatedAt = notification.CretaedAt,
                Title = notification.Title,
                Description = notification.Description,
                IsSeen = false
            };

            await _notificationHub.Clients.All.SendAsync("NewNotification", hubNotification);


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

    public async Task<Response<List<GetNotificationDto>>> GetAllNotifications()
    {
        Response<List<GetNotificationDto>> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var notifications = await _appUserNotificationRepository.GetAllNotifications(user.Id);
            if (notifications == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = notifications;
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

    public async Task<Response<bool>> MarkAllNotificationsAsRead()
    {
        Response<bool> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            var notSeenNotifications = await _appUserNotificationRepository.GetAllNotSeenNotifications(user.Id);
            if (notSeenNotifications == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (notSeenNotifications.Count <= 0)
            {
                response.Status = ResponseStatus.Success;
                response.Data = true;
                return response;
            }

            foreach (var notification in notSeenNotifications)
            {
                notification.IsSeen = true;
            }

            if (!await _appUserNotificationRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to mark notifications as seen.";
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

    public async Task<Response<int>> MarkNotificationAsRead(int notificationId)
    {
        Response<int> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            var userNotification = await _appUserNotificationRepository.GetUserNotification(user.Id, notificationId);
            if (userNotification == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (userNotification.IsSeen == false)
            {
                userNotification.IsSeen = true;
                if (!await _appUserNotificationRepository.SaveAllAsync())
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to mark notifications as read.";
                    return response;
                }
            }

            response.Status = ResponseStatus.Success;
            response.Data = userNotification.NotificationId;

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
