using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IOwnerImageService
{
    Task<Response<ImageDto>> UploadProfileImage(IFormFile image);
}

