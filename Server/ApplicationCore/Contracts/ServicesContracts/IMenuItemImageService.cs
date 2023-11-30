using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IMenuItemImageService
{
    Task<Response<ImageDto>> UploadProfileImage(int menuItemId, IFormFile image);
    Task<Response<int>> DeleteImage(int imageId);
}

