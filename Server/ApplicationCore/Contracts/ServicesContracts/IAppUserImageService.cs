using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IAppUserImageService
{
    Task<ImageDto> UploadProfileImage(int userId, IFormFile image);

    Task<ImageDto> GetUserProfileImage();
}

