using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IAppUserImageService
{
    Task<Response<ImageDto>> UploadProfileImage(IFormFile image);

    Task<ImageDto> GetUserProfileImage();
}

