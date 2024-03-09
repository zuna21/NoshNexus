namespace ApplicationCore;

public class FirebaseMessageDto
{
    public string Title { get; set; }
    public string Body { get; set; }
    public string DeviceToken { get; set; }
}

public class FcmTokenDto
{
    public string Token { get; set; }
}