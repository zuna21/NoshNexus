using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs.OwnerDtos;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers.UserControllers;

public class AccountController(
    IUserService userService
) : DefaultUserController
{
    private readonly IUserService _userService = userService;

    [HttpPost("login")]
    public async Task<ActionResult<AccountDto>> Login(LoginDto loginDto)
    {
        var response = await _userService.Login(loginDto);
        switch (response.Status)
        {
            case ResponseStatus.Unauthorized:
                return Unauthorized(response.Message);
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

    [Authorize]
    [HttpGet("refresh-user")]
    public async Task<ActionResult<AccountDto>> RefreshUser()
    {
        var response = await _userService.RefreshUser();
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

    [Authorize]
    [HttpGet("test")]
    public ActionResult Test()
    {
        return Unauthorized("proba tokena");
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AccountDto>> RefreshToken(RefreshTokenDto refreshTokenDto)
    {
        var response = await _userService.RefreshToken(refreshTokenDto);
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
                return Unauthorized("NVLD_SRNM");
        }
    }
}
