using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IOwnerService
{
    Task<Response<OwnerAccountDto>> Register(RegisterOwnerDto registerOwnerDto);
    Task<Response<OwnerAccountDto>> Login(LoginOwnerDto loginOwnerDto);
    Task<Response<int>> Update(EditOwnerDto editOwnerDto);
    Task<Response<GetOwnerDto>> GetOwnerDetails();
    Task<Response<GetOwnerEditDto>> GetOwnerEdit();
    Task<Response<ImageDto>> UploadProfileImage(IFormFile image);

}