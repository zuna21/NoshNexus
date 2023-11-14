namespace API;

public interface IEmployeeImageService
{
    Task<Response<bool>> UploadProfileImage(int employeeId, IFormFile image);
}
