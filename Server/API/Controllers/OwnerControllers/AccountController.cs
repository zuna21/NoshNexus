using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace API;
public class AccountController : DefaultOwnerController
{
    private readonly IOwnerService _ownerService;
    public AccountController(
        IOwnerService ownerService
    )
    {
        _ownerService = ownerService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<OwnerDtos.AccountDto>> Register(OwnerDtos.RegisterDto registerOwnerDto)
    {
        var response = await _ownerService.Register(registerOwnerDto);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong");
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<OwnerDtos.AccountDto>> Login(OwnerDtos.LoginDto loginOwnerDto)
    {
        var response = await _ownerService.Login(loginOwnerDto);
        switch (response.Status)
        {
            case ResponseStatus.Unauthorized:
                return Unauthorized(response.Message);
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpPost("upload-profile-image")]
    public async Task<ActionResult<ImageDto>> UploadProfileImage()
    {
        var image = Request.Form.Files[0];
        var response = await _ownerService.UploadProfileImage(image);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
