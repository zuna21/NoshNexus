namespace ApplicationCore;

public interface IFirebaseNotificationService
{
    Task<bool> SendOrderNotification(FirebaseMessageDto firebaseMessageDto);
}
