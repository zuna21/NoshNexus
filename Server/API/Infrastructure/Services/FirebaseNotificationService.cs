using ApplicationCore;
using FirebaseAdmin.Messaging;

namespace API;

public class FirebaseNotificationService : IFirebaseNotificationService
{
    public async Task<bool> SendOrderNotification(FirebaseMessageDto firebaseMessageDto)
    {
        var message = new Message()
        {
            Notification = new Notification
            {
                Title = firebaseMessageDto.Title,
                Body = firebaseMessageDto.Body
            },
            Token = firebaseMessageDto.DeviceToken
        };

        var messaging = FirebaseMessaging.DefaultInstance;
        var result = await messaging.SendAsync(message);

        if (!string.IsNullOrEmpty(result))
        {
           return true;
        }
        return false;
    }
}
