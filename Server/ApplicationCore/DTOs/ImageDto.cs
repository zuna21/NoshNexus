namespace ApplicationCore.DTOs;

public class ImageDto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public long Size { get; set; }
}

public class ChangeProfileImageDto
{
    public ImageDto NewProfileImage { get; set; }
    public ImageDto OldProfileImage { get; set; }
}