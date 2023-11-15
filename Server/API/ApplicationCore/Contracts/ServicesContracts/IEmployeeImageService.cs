namespace API;

public interface IEmployeeImageService
{
    Task<Response<ImageDto>> UploadProfileImage(int employeeId, IFormFile image);
}
