﻿namespace API;

public interface IOwnerImageService
{
    Task<Response<ImageDto>> UploadProfileImage(IFormFile image);
}