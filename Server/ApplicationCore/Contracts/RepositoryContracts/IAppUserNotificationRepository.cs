﻿using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IAppUserNotificationRepository
{
    void AddManyAppUserNotifications(List<AppUserNotification> appUserNotifications);
    Task<List<GetNotificationDto>> GetLastNotifications(int userId, int notificationsNumber);
    Task<AppUserNotification> GetUserNotification(int userId, int notificationId);
    Task<List<AppUserNotification>> GetAllNotSeenNotifications(int userId);
    Task<List<GetNotificationDto>> GetAllNotifications(int userId);
    Task<int> CountNotSeenNotifications(int userId);
    Task<bool> SaveAllAsync();
}

