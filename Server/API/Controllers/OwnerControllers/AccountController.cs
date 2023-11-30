using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API;
public class AccountController : DefaultOwnerController
{
    private readonly IOwnerService _ownerService;
    private readonly IAppUserImageService _appUserImageService;
    public AccountController(
        IOwnerService ownerService,
        IAppUserImageService appUserImageService
    )
    {
        _ownerService = ownerService;
        _appUserImageService = appUserImageService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<OwnerAccountDto>> Register(RegisterOwnerDto registerOwnerDto)
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
    public async Task<ActionResult<OwnerAccountDto>> Login(LoginOwnerDto loginOwnerDto)
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
        var response = await _appUserImageService.UploadProfileImage(image);
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
