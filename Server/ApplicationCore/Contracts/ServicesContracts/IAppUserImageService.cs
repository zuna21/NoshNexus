using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IAppUserImageService
{
    Task<Response<ImageDto>> UploadProfileImage(IFormFile image);
    Task<Response<ImageDto>> UploadEmployeeProfileImage(int employeeId, IFormFile image);
}

