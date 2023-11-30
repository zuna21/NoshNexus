using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IEmployeeImageService
{
    Task<Response<ImageDto>> UploadProfileImage(int employeeId, IFormFile image);
}

